using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Telegram.Bot;

namespace SendMessage
{
    /// <summary>
    /// Логика взаимодействия для PageContactSelect.xaml
    /// </summary>
    public partial class PageContactSelect : Page
    {
        private static string sqlConnect { get; set; } = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\Георгий\\Documents\\Проект по учебной практике\\SendMessage\\SendMessage\\Database1.mdf\";Integrated Security=True";
        private static string token { get; set; } = "6744752309:AAFt9-0ygv3RzDD9TWaoB36kPBzm05Em8Tg";

        private static TelegramBotClient client;

        public class Users
        {
            public int Id { get; set; }
            public string userName { get; set; }
            public string ChatId { get; set; }

            public bool? CheckBoxColumn { get; set; }
        }
        public PageContactSelect()
        {
            InitializeComponent();
            SqlConnection sqlConnection = new SqlConnection(sqlConnect);
            sqlConnection.Open();
            SqlDataAdapter dataAdapter = new SqlDataAdapter($"SELECT * FROM [Users]", sqlConnect);
            DataSet dataSet = new DataSet();

            dataAdapter.Fill(dataSet);

            DataTable table = dataSet.Tables[0];
            List<Users> students = table.AsEnumerable().Select(row =>
                new Users
                {
                    Id = row.Field<int>("Id"),
                    userName = row.Field<string>("userName"),
                    ChatId = row.Field<string>("ChatId"),
                }).ToList();
            //Убираю дубликаты 
            List<Users> distinctUsers = students.GroupBy(user => user.userName)
                                 .Select(group => group.First())
                                 .ToList();

            dataStud.ItemsSource = distinctUsers;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            client = new TelegramBotClient(token);

            client.StartReceiving();
            // Получаем выделенные строки
            var selectedItems = dataStud.Items;

            foreach (var item in selectedItems)
            {
                // Приводим каждый элемент к типу User
                var user = item as Users;

                if (user != null && user.CheckBoxColumn != null && user.CheckBoxColumn.Value != null && user.CheckBoxColumn.Value)
                {
                    // Делаем что-то с пользователем, у которого CheckBox выбран
                    using (SqlConnection connection = new SqlConnection(sqlConnect))
                    {
                        connection.Open();

                        using (SqlCommand command = new SqlCommand("INSERT INTO [dbo].[SelectContactTelegram] ( userName,ChatId ) VALUES ( @userName,@ChatId)", connection))
                        {


                            command.Parameters.AddWithValue("@userName", user.userName);
                            command.Parameters.AddWithValue("@ChatId", user.ChatId);

                            command.ExecuteNonQuery();

                            command.Parameters.Clear();





                        }
                    }
                }
                MessageBox.Show("Сообщения отправлены");
            }
        }
    }
}
