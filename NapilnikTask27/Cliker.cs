using System;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace NapilnikTask27
{
    internal class Cliker
    {
        private object _passportTextbox;
        private object _textResult;

        private void CheckButton_Click(object sender, EventArgs e)
        {
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
                        SQLiteDataAdapter sqLiteDataAdapter = new SQLiteDataAdapter(new SQLiteCommand(commandText, connection));
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
