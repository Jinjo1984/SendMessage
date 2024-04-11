using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
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
using TdLib;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;

namespace SendMessage
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static string token { get; set; } = "6744752309:AAFt9-0ygv3RzDD9TWaoB36kPBzm05Em8Tg";
        private static TelegramBotClient client;
        //app_id=24885848
        //app_hash=cada1e625232f1c74cf84857617290a5
        public MainWindow()
        {
            InitializeComponent();
            //client = new TelegramBotClient(token);
            //client.OnMessage += Client_OnMessage1;
            //client.StartReceiving();
            MainFrame.Content = new PageAddUserTelegram();


        }

        //private void Client_OnMessage1(object sender, MessageEventArgs e)
        //{
        //    var message = e.Message;
        //    TimeSpan TimeMess = DateTime.UtcNow - e.Message.Date;
        //    long s = message.Chat.Id;
        //}

        
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new PageAddUserTelegram();

        }

        private void SendMess_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new PageContactSelect();
        }

        private void AddMail_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new PageAddMail();
        }
    }

    }
