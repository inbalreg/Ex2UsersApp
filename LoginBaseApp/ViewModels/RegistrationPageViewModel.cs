using LoginBaseApp.Helper;
using LoginBaseApp.Service;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace LoginBaseApp.ViewModels
{
    public class RegistrationPageViewModel : ViewModelBase
    {

        // שדה פרטי לשמירת שירות ההתחברות שהוזרק
        private readonly ILoginService db;
        /// <summary>
        /// בנאי של ה-ViewModel.
        /// </summary>
        /// <param name="service">שירות ההתחברות שיוזרק באמצעות Dependency Injection.</param>
        public RegistrationPageViewModel(ILoginService loginService)
        {
            db = loginService;
            // אתחול הפקודות והגדרת ערכים ראשוניים
            ShowPasswordCommand = new Command(TogglePasswordVisiblity);
            LoginCommand = new Command(Login, CanLogin);
            ShowPasswordIcon = FontHelper.CLOSED_EYE_ICON; // הגדרת אייקון ברירת מחדל
            IsPassword = true; // הגדרת שדה הסיסמה כמוסתר כברירת מחדל

            _date = DateTime.Today;
        }

        private string? _userName;
        private string? _password;
        private string? _name;
        private string? _email;
        private string? _phoneNum;
        private DateTime? _date;


        /// <summary>
        /// שם המשתמש המוזן על ידי המשתמש ב-UI.
        /// </summary>
        public string? UserName
        {
            get => _userName;
            set
            {
                if (_userName != value)
                {
                    _userName = value;
                    OnPropertyChanged(); // מודיע ל-UI על שינוי כדי לעדכן את התצוגה
                    (LoginCommand as Command)?.ChangeCanExecute(); // בודק מחדש אם ניתן להפעיל את כפתור ההתחברות
                }
            }
        }

        /// <summary>
        /// הסיסמה המוזנת על ידי המשתמש ב-UI.
        /// </summary>
        public string? Password
        {
            get => _password;
            set
            {
                if (_password != value)
                {
                    _password = value;
                    OnPropertyChanged(); // מודיע ל-UI על שינוי
                    (LoginCommand as Command)?.ChangeCanExecute(); // בודק מחדש אם ניתן להפעיל את כפתור ההתחברות
                }
            }
        }

        /// <summary>
        /// Gets or sets the name associated with the object.
        /// </summary>
        /// <remarks>Setting this property raises the <c>PropertyChanged</c> event if the value
        /// changes.</remarks>
        public string? Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged();
                }
            }
        }

        public string? Email
        {
            get => _email;
            set
            {
                if (_email != value)
                {
                    _email = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime? SelectedDate
        {
            get { return _date;  }
            set {
                    if (_date != value)
                    {
                        _date = value;
                        OnPropertyChanged();
                    }
                }
            }

    public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public int Age
        {
            get {
                DateTime currentDate = DateTime.Today;
                DateTime? selectedDate = _date;
                TimeSpan ts = selectedDate - currentDate;
                return (TimeSpan)(selectedDate - currentDate). Days;
            }
        } 

        public string? PhoneNum
        {
            get => _phoneNum;
            set
            {
                if (_phoneNum != value)
                {
                    _phoneNum = value;
                    OnPropertyChanged();
                }
            }
        }

        //public string? Date
        //{
        //    get => _date;
        //    set
        //    {
        //        if (_date != value)
        //        {
        //            _date = value;
        //            OnPropertyChanged();
        //        }
        //    }
        //}

        private bool messageIsVisible;
        /// <summary>
        /// קובע אם הודעת המשוב (הצלחה/שגיאה) תוצג למשתמש.
        /// </summary>
        public bool MessageIsVisible
        {
            get => messageIsVisible;
            set
            {
                if (messageIsVisible != value)
                {
                    messageIsVisible = value;
                    OnPropertyChanged();
                }
            }
        }

        private Color? messageColor;
        /// <summary>
        /// קובע את צבע הודעת המשוב (למשל, ירוק להצלחה ואדום לשגיאה).
        /// </summary>
        public Color? MessageColor
        {
            get => messageColor;
            set
            {
                if (messageColor != value)
                {
                    messageColor = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool isPassword;
        /// <summary>
        /// קובע האם שדה הסיסמה יוצג כמוסתר (true) או גלוי (false).
        /// </summary>
        public bool IsPassword
        {
            get => isPassword;
            set
            {
                if (isPassword != value)
                {
                    isPassword = value;
                    OnPropertyChanged();
                }
            }
        }

        private string? showPasswordIcon;
        /// <summary>
        /// האייקון שיוצג עבור כפתור הצג/הסתר סיסמה.
        /// </summary>
        public string? ShowPasswordIcon
        {
            get => showPasswordIcon;
            set
            {
                if (showPasswordIcon != value)
                {
                    showPasswordIcon = value;
                    OnPropertyChanged();
                }
            }
        }

        private string? loginMessage;
        /// <summary>
        /// טקסט הודעת המשוב שתוצג למשתמש לאחר ניסיון התחברות.
        /// </summary>
        public string? LoginMessage
        {
            get => loginMessage;
            set
            {
                if (loginMessage != value)
                {
                    loginMessage = value;
                    OnPropertyChanged();
                }
            }
        }


        /// <summary>
        /// תנאי הקובע אם ניתן להפעיל את פקודת ההתחברות.
        /// </summary>
        /// <returns>אמת אם גם שם המשתמש וגם הסיסמה אינם ריקים.</returns>
        public bool CanLogin()
        {
            return (!string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(Password));
        }

        /// <summary>
        /// פקודה להצגה והסתרה של הסיסמה.
        /// </summary>
        public ICommand ShowPasswordCommand
        {
            get;
        }

        /// <summary>
        /// פקודה לביצוע תהליך ההתחברות.
        /// </summary>
        public ICommand LoginCommand
        {
            get;
        }

        /// <summary>
        /// מחליף את מצב התצוגה של הסיסמה (מוסתר/גלוי) ומעדכן את האייקון בהתאם.
        /// </summary>
        private void TogglePasswordVisiblity()
        {
            IsPassword = !IsPassword; // הופך את הערך הבוליאני
            if (IsPassword)
                ShowPasswordIcon = FontHelper.CLOSED_EYE_ICON;
            else
                ShowPasswordIcon = FontHelper.OPEN_EYE_ICON;
        }

        /// <summary>
        /// מבצע את לוגיקת ההתחברות.
        /// </summary>
        private void Login()
        {
            IsBusy = true; // מסמן שהאפליקציה בתהליך (להצגת מחוון טעינה)
            MessageIsVisible = true; // מציג את אזור הודעת המשוב

            // קורא לשירות ההתחברות עם הפרטים שהוזנו
            if (db.Login(UserName!, Password!))
            {
                // במקרה של הצלחה
                LoginMessage = AppMessages.LoginMessage;
                MessageColor = Colors.Green;
                // כאן ניתן להוסיף ניווט לדף הבא
            }
            else
            {
                // במקרה של כישלון
                LoginMessage = AppMessages.LoginErrorMessage;
                MessageColor = Colors.Red;
            }
            IsBusy = false; // מסיים את מצב "עסוק"
        }


    }
}