using ChatWithMeWindows.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

public class LoginViewModel : ViewModelBase
{
    private readonly HttpService _httpService;
    private readonly NavigationService _navigationService;
    private readonly StorageService _storageService;

    private string _username;
    private string _password;
    private bool _isLoading;
    private string _errorMessage;

    public string Username
    {
        get => _username;
        set => SetProperty(ref _username, value);
    }

    public string Password
    {
        get => _password;
        set => SetProperty(ref _password, value);
    }

    public bool IsLoading
    {
        get => _isLoading;
        set => SetProperty(ref _isLoading, value);
    }

    public string ErrorMessage
    {
        get => _errorMessage;
        set => SetProperty(ref _errorMessage, value);
    }

    public ICommand LoginCommand { get; }
    public ICommand NavigateToRegisterCommand { get; }

    public LoginViewModel(HttpService httpService, NavigationService navigationService, StorageService storageService)
    {
        _httpService = httpService;
        _navigationService = navigationService;
        _storageService = storageService;

        LoginCommand = new RelayCommand(async () => await LoginAsync(), CanLogin);
        NavigateToRegisterCommand = new RelayCommand(() => _navigationService.NavigateTo("RegisterPage"));
    }

    private bool CanLogin()
    {
        return !string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password);
    }

    private async Task LoginAsync()
    {
        try
        {
            IsLoading = true;
            ErrorMessage = string.Empty;

            var user = await _httpService.LoginAsync(Username, Password);

            _storageService.SaveUser(user);

            _navigationService.NavigateTo("MainPage");
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.ToString();
        }
        finally
        {
            IsLoading = false;
        }
    }
}