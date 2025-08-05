using LoginBaseApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace LoginBaseApp.Service
{
	/// <summary>
	/// מימוש של שירות ההתחברות המדמה קריאה ל-API חיצוני.
	/// </summary>
	public class ApiService : ILoginService
	{
		/// <summary>
		/// בנאי של שירות ה-API.
		/// כאן ניתן להגדיר תלויות כמו HttpClient או הגדרות תצורה.
		/// </summary>
		public ApiService()
		{
			// Initialize any necessary services or configurations here
		}

		/// <summary>
		/// מבצע אימות פרטי משתמש מול שירות API.
		/// </summary>
		/// <param name="username">שם המשתמש.</param>
		/// <param name="password">סיסמת המשתמש.</param>
		/// <returns>אמת (true) אם ההתחברות הצליחה, אחרת שקר (false).</returns>
		public bool Login(string username, string password)
		{
			// זהו מימוש דמה (placeholder). בלוגיקה אמיתית, תתבצע כאן קריאת רשת
			// לשרת שיאמת את הפרטים ויחזיר תשובה בהתאם.
			return true;
		}

        public bool Register(string username, string password)
        {
            throw new NotImplementedException();
        }

        bool ILoginService.deleteUserByUsername(string username)
        {
            throw new NotImplementedException();
        }

        User? ILoginService.getUserByUsername(string username)
        {
            throw new NotImplementedException();
        }

        bool ILoginService.Register(string name, string username, string password, string email, string phoneNum, DateTime date)
        {
            throw new NotImplementedException();
        }

        
    }
}