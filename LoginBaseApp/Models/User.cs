using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginBaseApp.Models;

/// <summary>
/// מייצג את מודל הנתונים של משתמש באפליקציה.
/// </summary>
public class User
{
	/// <summary>
	/// Represents the name associated with the current instance.
	/// </summary>
	/// <remarks>This member is private and is not directly accessible outside the class.  Use the appropriate public
	/// properties or methods to interact with this value.</remarks>
    private string name;

    public string? Name
    {
        get { return name; }
        set { 
			
			if(value == null)
				throw new ArgumentNullException(nameof(value), "Name cannot be null.");
            name = value; 
		
		}
    }

	/// <summary>
	/// שם המשתמש לצורך הזדהות.
	/// </summary>
	private string username;
    public string? Username
	{
		get { return username; }
		set { 
			if (string.IsNullOrWhiteSpace(value))
				throw new ArgumentException("Username cannot be null or whitespace.", nameof(value));
			username = value;
        }
	}

    /// <summary>
    /// סיסמת המשתמש.
    /// </summary>
    private string password;
    public string? Password
	{
		get { return password;  }
		set
		{
			if (string.IsNullOrWhiteSpace(value))
				throw new ArgumentException("Password cannot be null or whitespace.", nameof(value));
			password = value;
        }
	}

	private EmailAddress email;

	public string Email
	{
		get { return email.Email; }
		set
		{
				if (email == null)
					email = new EmailAddress(value);
				else email.Email = value;
		}
    }

    private string phoneNum;
    public string? PhoneNum
	{
		get { return phoneNum; }
		set
		{
			if (string.IsNullOrWhiteSpace(value))
				throw new ArgumentException("Phone number cannot be null or whitespace.", nameof(value));
			phoneNum = value;
		}
    }

	private DateTime birthDate;

	public DateTime BirthDate
    {
		get { return birthDate; }
		set { birthDate = value; }
	}

    public User(string name, string username,string password, string emailStr, string phoneNum, DateTime birthDate)
    {
        Name = name;
        Username = username;
        Password = password;
        Email = emailStr;
        PhoneNum = phoneNum;
		//if(birthDate != null)
		BirthDate = (DateTime)birthDate;
    }

	public User() { }
    //string name, string username, string password, string email, string phoneNum, Date date
}