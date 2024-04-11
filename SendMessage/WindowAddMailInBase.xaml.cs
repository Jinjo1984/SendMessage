using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SendMessage
{
    /// <summary>
    /// Логика взаимодействия для WindowAddMailInBase.xaml
    /// </summary>
    public partial class WindowAddMailInBase : Window
    {
        private static string sqlConnect { get; set; } = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\Георгий\\Documents\\Проект по учебной практике\\SendMessage\\SendMessage\\Database1.mdf\";Integrated Security=True";

        public WindowAddMailInBase()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string textBoxmail = MailtextBox.Text;
            string textboxName = NameTextBox.Text;
           if(textBoxmail != null && textboxName != null && IsValidEmail(textBoxmail))
            {
                using (SqlConnection connection = new SqlConnection(sqlConnect))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("INSERT INTO [dbo].[AddMail] ( Name,Mail ) VALUES ( @userName,@Mail)", connection))
                    {
                        
                        
                            command.Parameters.AddWithValue("@userName", textboxName);
                            command.Parameters.AddWithValue("@Mail", textBoxmail);

                            command.ExecuteNonQuery();

                            command.Parameters.Clear();
                        




                    }

                }
            }
        }
        public static bool IsValidEmail(string email)
        {
            // Регулярное выражение для проверки электронной почты
            string pattern = @"^[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]{2,7}$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(email);
        }
    }
}
