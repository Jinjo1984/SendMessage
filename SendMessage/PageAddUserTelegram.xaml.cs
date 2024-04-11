using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
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
    /// Логика взаимодействия для PageAddUserTelegram.xaml
    /// </summary>
    public partial class PageAddUserTelegram : Page
    {
        public PageAddUserTelegram()
        {
            InitializeComponent();
            client = new TelegramBotClient(token);
            client.OnMessage += Client_OnMessage;
            client.StartReceiving();
            dataStud.ItemsSource = users;
        }

        private static string token { get; set; } = "6744752309:AAFt9-0ygv3RzDD9TWaoB36kPBzm05Em8Tg";
        private static string sqlConnect { get; set; } = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\Георгий\\Documents\\Проект по учебной практике\\SendMessage\\SendMessage\\Database1.mdf\";Integrated Security=True";

        private static TelegramBotClient client;
        public static ObservableCollection<Telegram.Bot.Types.Message> users = new ObservableCollection<Telegram.Bot.Types.Message>();

        public static Label NameUserLabell;

        
        private static async void Client_OnMessage(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            //  var addUsersTelegram = new AddUsersTelegram();
            var message = e.Message;
            TimeSpan TimeMess = DateTime.UtcNow - e.Message.Date;
            // string name = (message.Chat.Username)

            if (message != null && TimeMess.Seconds < 5)
            {

                Application.Current.Dispatcher.Invoke(() =>
                {

                    users.Add(message);


                });

                foreach (var user in users)
                {

                }

            }

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(sqlConnect))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("INSERT INTO [dbo].[Users] ( UserName,ChatId ) VALUES ( @userName,@ChatId)", connection))
                {
                    for (int i = 0; i < users.Count; i++)
                    {
                        command.Parameters.AddWithValue("@userName", users[i].Chat.FirstName);
                        command.Parameters.AddWithValue("@ChatId", users[i].Chat.Id);

                        command.ExecuteNonQuery();

                        command.Parameters.Clear();
                    }




                }
               
            }
        }
    }
}

