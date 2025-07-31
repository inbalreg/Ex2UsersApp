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
	public class DBMokup : ILoginService
	{
		// רשימה המשמשת כמסד נתונים מדמה
		List<Models.User> users = new List<Models.User>();

		/// <summary>
		/// בנאי המאתחל את "מסד הנתונים" עם משתמשים לדוגמה.
		/// </summary>
		public DBMokup()
		{
			users.Add(new Models.User { Username = "admin", Password = "admin" });
			users.Add(new Models.User { Username = "user1", Password = "password1" });
			users.Add(new Models.User { Username = "user2", Password = "password2" });
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

        public bool Register(string name, string username, string password, string email, string phoneNum, string date)
        {
            try
            {
               User newUser = new User
				{
					Name = name,
					Username = username,
					Password = password,
					Email = email,
					PhoneNum = phoneNum,
					//Date = date
				};
				// בדיקה אם המשתמש כבר קיים
				if (users.Any(u => u.Username == username))
				{
					throw new ArgumentException("User already exists with this username.", nameof(username));
                    //return false; // משתמש קיים, רישום נכשל
                }
				// הוספת המשתמש לרשימה
				users.Add(newUser);
				return true; // רישום הצליח
            }
            catch (Exception ex)
            {
                return false; // Add a return statement in catch block
            }
        }

        bool ILoginService.deleteUserByUsername(string username)
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
        User? ILoginService.getUserByUsername(string username)
        {
            User user = users.FirstOrDefault(u => u.Username == username);
			return user; // מחזיר את המשתמש אם נמצא, אחרת מחזיר null
        }

    
    }
}