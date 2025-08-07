using LoginBaseApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginBaseApp.Service
{
	/// <summary>
	/// מימוש של שירות ההתחברות המשתמש ברשימת משתמשים מקומית (Mock) לצורכי פיתוח ובדיקה.
	/// </summary>
	public class DBMokup : IUserServices
	{
		// רשימה המשמשת כמסד נתונים מדמה
		List<Models.User> users = new List<Models.User>();

		/// <summary>
		/// בנאי המאתחל את "מסד הנתונים" עם משתמשים לדוגמה.
		/// </summary>
		public DBMokup()
		{
			users.Add(new Models.User("admin", "admin1", "ad1",  "adminMail1", "12376543", new DateTime(1909, 5, 15)));// Username = "admin", Password = "admin", Name = "ad", PhoneNum = "123", Email = "abc", BirthDate = DateTime.Now });
			users.Add(new Models.User ( "user1", "password1", "us1",  "usMail2", "12376543", new DateTime(1980, 9, 15)));
			users.Add(new Models.User ( "user2", "password2", "us2", "myMail3", "123", new DateTime(2000, 5, 15)  ));//Username = "user2", Password = "password2", Name = "us2", PhoneNum = "123",BirthDate = DateTime.Now });
		}

		/// <summary>
		/// מבצע אימות פרטי משתמש מול רשימת המשתמשים המקומית.
		/// </summary>
		/// <param name="username">שם המשתמש לבדיקה.</param>
		/// <param name="password">הסיסמה לבדיקה.</param>
		/// <returns>אמת (true) אם נמצא משתמש עם פרטים תואמים, אחרת שקר (false).</returns>
		public bool Login(string username, string password)
		{
			// חיפוש המשתמש הראשון ברשימה שתואם לשם המשתמש והסיסמה שהתקבלו
			var user = users.FirstOrDefault(u => u.Username == username && u.Password == password);
			// אם נמצא משתמש (התוצאה אינה null), ההתחברות הצליחה
			return user != null;
		}

        public bool Register(string name, string username, string password, string email, string phoneNum, DateTime date)
        {
            try
            {
					var newUser = new User
					{

						Name = name,
						Username = username,
						Password = password,
						Email = email,
						PhoneNum = phoneNum,
						BirthDate = date
					};
					// בדיקה אם המשתמש כבר קיים
					if (users.Any(u => u.Username == username))
					{
						throw new ArgumentException("User already exists with this username.");
						//return false; // משתמש קיים, רישום נכשל
					}

				// הוספת המשתמש לרשימה
				users.Add(newUser);
				return true; // רישום הצליח
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
				throw new ArgumentException(ex.Message);
				//return false; // Add a return statement in catch block
            }
        }

        bool IUserServices.deleteUserByUsername(string username)
        {
            User user = users.FirstOrDefault(u => u.Username == username);
            if (user == null)
            {
                return false; // משתמש לא נמצא, מחיקה נכשלה
            }
            else
            {
                users.Remove(user);
                return true; // משתמש נמחק בהצלחה
            }
        }

		/// <summary>
		/// Get a username string and return the user object if exists
		/// </summary>
		/// <param name="username"></param>
		/// <returns></returns>
        User? IUserServices.getUserByUsername(string username)
        {
            User? user = users.FirstOrDefault(u => u.Username == username);
			return user; // מחזיר את המשתמש אם נמצא, אחרת מחזיר null
        }

        List<User> IUserServices.GetUsers()
        {
            return users; // מחזיר את רשימת כל המשתמשים	
        }
    }
}