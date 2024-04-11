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
using Microsoft.SqlServer.Server;
using Microsoft.VisualBasic;
namespace SendMessage
{
    /// <summary>
    /// Логика взаимодействия для PageAddMail.xaml
    /// </summary>
    public partial class PageAddMail : Page
    {
        private static string sqlConnect { get; set; } = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\Георгий\\Documents\\Проект по учебной практике\\SendMessage\\SendMessage\\Database1.mdf\";Integrated Security=True";

        public PageAddMail()
        {
            InitializeComponent();

            SqlConnection sqlConnection = new SqlConnection(sqlConnect);
            sqlConnection.Open();
            SqlDataAdapter dataAdapter = new SqlDataAdapter($"SELECT * FROM [AddMail]", sqlConnect);
            DataSet dataSet = new DataSet();

            dataAdapter.Fill(dataSet);

            DataTable table = dataSet.Tables[0];
            List<MailUser> students = table.AsEnumerable().Select(row =>
                new MailUser
                {
                    Id = row.Field<int>("Id"),
                    userName = row.Field<string>("Name"),
                    Mail = row.Field<string>("Mail"),
                }).ToList();
        dataMail.ItemsSource = students;
        }
        class MailUser
        {
            public int Id { get; set; }
            public string userName { get; set; }
            public string Mail { get; set; }

            public bool? CheckBoxColumn { get; set; }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            WindowAddMailInBase windowAddMailInBase= new WindowAddMailInBase();
            windowAddMailInBase.Show();

        }
    }
}
