using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NapilnikTask27
{
    public partial class Form1 : Form
    {
        private object _passportTextbox;
        private object _textResult;

        public Form1()
        {
            InitializeComponent();
        }

        internal static object ComputeSha256Hash(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                StringBuilder builder = new StringBuilder();
                
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("xY"));
                }
                return builder.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _passportTextbox = textBox1.Text;

            if (_passportTextbox.ToString().Trim() == "")
            {
                int num1 = (int)MessageBox.Show("Введите серию и номер паспорта");
            }
            else
            {
                string rawData = _passportTextbox.ToString().Trim().Replace(" ", string.Empty);

                if (rawData.Length < 10)
                {
                    _textResult = "Неверный формат серии или номера паспорта";
                }
                else
                {
                    string commandText = string.Format("select * from passports where num='{0}' limit 1;", (object)Form1.ComputeSha256Hash(rawData));
                    string connectionString = string.Format("Data Source=" + Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\db.sqlite");
                    try
                    {
                        SQLiteConnection connection = new SQLiteConnection(connectionString);
                        connection.Open();
                        SQLiteDataAdapter sqLiteDataAdapter = new SQLiteDataAdapter(new SQLiteConnection(commandText), connection);
                        DataTable dataTable1 = new DataTable();
                        DataTable dataTable2 = dataTable1;
                        sqLiteDataAdapter.Fill(dataTable2);
                        if (dataTable1.Rows.Count > 0)
                        {
                            if (Convert.ToBoolean(dataTable1.Rows[0].ItemArray[1]))
                                _textResult = "По паспорту «" + _passportTextbox.ToString() + "» доступ к бюллетеню на дистанционном электронном голосовании ПРЕДОСТАВЛЕН";
                            else
                                _textResult = "По паспорту «" + _passportTextbox.ToString() + "» доступ к бюллетеню на дистанционном электронном голосовании НЕ ПРЕДОСТАВЛЯЛСЯ";
                        }
                        else
                            _textResult = "Паспорт «" + _passportTextbox.ToString() + "» в списке участников дистанционного голосования НЕ НАЙДЕН";
                        connection.Close();
                    }
                    catch (SQLiteException ex)
                    {
                        if (ex.ErrorCode != 1)
                            return;
                        int num2 = (int)MessageBox.Show("Файл db.sqlite не найден. Положите файл в папку вместе с exe.");
                    }
                }
            }
        }
    }
}
