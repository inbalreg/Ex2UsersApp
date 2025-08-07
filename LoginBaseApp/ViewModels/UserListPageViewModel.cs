//using IntelliJ.Lang.Annotations;
using System.Collections.ObjectModel;
using System.Windows.Input;
using LoginBaseApp.Models;
using LoginBaseApp.Service;

namespace LoginBaseApp.ViewModels;

public class UserListPageViewModel : ViewModelBase
{

    #region Fields
    IUserService _service;
    List<User> _allUsers = new();
    //Task loadDataTask;
    string _searchText;
    bool _hasError = false;
    #endregion

    #region

    public UserListPageViewModel(IUserServices service)
    {
        _service = service as DBMokup;
        // Initialize properties or commands here if needed
        SearchCommand = new Command(OnSearch);
        ClearFilterCommand = new Command(ClearFilter, () => string.IsNullOrEmpty(SearchText));
        LoadDataCommand = new Command(async () => await GetUsersAsync()); //GetTasksAsync(1));
        // Load all tasks from the service
        ////*****//////loadDataTask = GetTasksAsync(1);
        //DeleteTaskCommand = new Command<User>(DeleteTask);
        //AllTasks = new();
    }

    //{
    //	Content = new VerticalStackLayout
    //	{
    //		Children = {
    //			new Label { HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center, Text = "Welcome to .NET MAUI!"
    //			}
    //		}
    //	};

    public ObservableCollection<User> AllUsers
    {
        get; set;
    }
    public bool HasError
    {
        get => _hasError;
        set
        {
            if (_hasError != value)
            {
                _hasError = value;
                OnPropertyChanged();
            }
        }
    }

    public string SearchText
    {
        get => _searchText;
        set
        {
            if (_searchText != value)
            {
                _searchText = value;
                OnPropertyChanged();
                // Update the command state when SearchText changes
                (ClearFilterCommand as Command)?.ChangeCanExecute();
            }
        }
    }
    #endregion

    #region Commands
    // Define commands for user interactions, e.g., search, add task, etc.

    public ICommand DeleteUserCommand
    {
        get;
    }
    public ICommand SearchCommand
    {
        get;
    }
    public ICommand ClearFilterCommand
    {
        get;
    }

    public ICommand LoadDataCommand
    {
        get;
    }
    #endregion

   

    private void DeleteUser(User user)
    {
        if (user == null) return; // Ensure task is not null
        AllUsers.Remove(user);
    }

    private async Task<User?> GetUsersAsync()//int userId)
    {
        IsBusy = true;
        try
        {
            _allUsers = await _service.GetUsers();
            // Clear the existing collection and add the new tasks
            AllUsers.Clear();
            foreach (var task in _allUsers)
            {
                AllUsers.Add(task);
            }
            IsBusy = false;
        }
        catch (Exception ex)
        {
            HasError = true; // Set error state if an exception occurs	
                             // Handle exceptions, e.g., log the error or show a message to the user
            Console.WriteLine($"Error loading tasks: {ex.Message}");
        }
        finally
        {//*****
            //loadDataTask = null; // Reset the task after loading
            OnPropertyChanged(nameof(_allUsers)); // Notify that tasks have been loaded
            IsBusy = false;

        }
    }

    private void ClearFilter()
    {
        throw new NotImplementedException();
    }

    private void OnSearch()
    {
        throw new NotImplementedException();
    }
    // Add properties, commands, and methods for the UserTasksPage functionality
}

internal interface IUserService
{
}