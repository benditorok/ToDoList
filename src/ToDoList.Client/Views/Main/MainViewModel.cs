﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using ToDoList.Client.Services.Connection;
using ToDoList.Client.Views.Identity;
using ToDoList.Client.Views.NoteLists;
using ToDoList.Domain.Entities;

namespace ToDoList.Client.Views.Main;

public partial class MainViewModel : ObservableObject
{
    private AuthorizedConnectionService _connectionService;
    private ILogger<MainViewModel>? _logger;
    private static SemaphoreSlim _sm = new SemaphoreSlim(1, 1);

    [ObservableProperty]
    private NoteList? _userToDoList;

    [ObservableProperty]
    private Note _userToDoItem = new();

    public MainViewModel(AuthorizedConnectionService connectionService, ILogger<MainViewModel>? logger)
    { 
        _connectionService = connectionService;
        _logger = logger;
    }

    public async void OnPageLoaded(object? sender, EventArgs e)
    {
        if (_connectionService.IsLoggedIn)
        {
            await LoadToDoListAsync();
        }
        else
        {
            var stack = Shell.Current.Navigation.NavigationStack.ToArray();

            for (int i = stack.Length - 1; i > 0; i--)
            {
                Shell.Current.Navigation.RemovePage(stack[i]);
            }

            await Shell.Current.GoToAsync(nameof(AccountPage));
        }
    }

    private async Task LoadToDoListAsync()
    {
        try
        {
            const string title = "ToDoList";

            if (UserToDoList == null)
            {
                List<NoteList>? userLists;
                NoteList? todoList;

                await _sm.WaitAsync();

                try
                {
                    userLists = await _connectionService.GetAsync<List<NoteList>>("user/getallnotelists");
                    todoList = userLists?.OrderBy(x => x.Id).FirstOrDefault(x => x.Title == title);

                    if (todoList != null)
                    {
                        UserToDoList = todoList;
                    }
                    else
                    {
                        NoteList newTodoList = new() { Title = title };
                        await _connectionService.PostAsync<NoteList>("user/addnotelist", newTodoList);
                    }
                }
                finally
                {
                    _sm.Release();
                }

                if(todoList == null)
                {
                    await LoadToDoListAsync();
                }
            }
        }
        catch (Exception ex)
        {
            _logger?.LogInformation("[VM-MAIN] {ex}", ex.Message);

            if (await Shell.Current.DisplayAlert("Alert", "Loading the ToDoList failed!", "Retry", "Exit"))
            {
                await LoadToDoListAsync();
            }
            else
            {
                Application.Current?.Quit();
            }
        }
    }

    private async Task RefreshToDoListAsync()
    {
        try
        {
            var list = await _connectionService.GetAsync<NoteList>($"user/getnotelist?id={UserToDoList?.Id}");
            UserToDoList = list ?? UserToDoList;
        }
        catch (Exception ex)
        {
            _logger?.LogInformation("[VM-MAIN] {ex}", ex.Message);
        }
    }

    [RelayCommand]
    private async Task QuickAddNoteAsync()
    {
        try
        {
            await _connectionService.PostAsync<Note>($"user/addnote?listId={UserToDoList?.Id}", UserToDoItem);
        }
        catch (Exception ex)
        {
            _logger?.LogInformation("[VM-MAIN] {ex}", ex.Message);
        }
        finally
        {
            UserToDoItem = new();
            await RefreshToDoListAsync();
        }
    }

    [RelayCommand]
    private async Task GotoNoteListPageAsync()
    {
        await Shell.Current.GoToAsync(nameof(NoteListPage));
    }
}
