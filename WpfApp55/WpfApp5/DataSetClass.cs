using System.Collections;       //Пространство имён, необходимое для обращение к классу ArrayList
using System.Windows;
using System.Data;              //Пространство имён для работы с кэш таблицами, строками, столбцами и данными
using System.Data.SqlClient;

namespace WpfApp5
{
    class DataSetClass
    {
        /// <summary>
        /// Статические глобальные переменные для работы с источником данных в базе данных, где DS - Data Source, IC - Initial Catalog
        /// </summary>
        public static string DS = "null", IC = "null";
        /// <summary>
        /// Экземпляр класса для работы с источником данных
        ///Data Source - название персонального компьютера и название экземпляра SQL сервера
        /// Initial Catalog - название файла базы данных
        /// Integrated Security - аутентификация пользователей со стороны Windows
        /// </summary>
        private SqlConnection connection = new SqlConnection(string.Format("Data Source = {0}; Initial Catalog = {1}; Integrated Security = true;", DS, IC));
        ///Data Source - название персонального компьютера и название экземпляра SQL сервера
        /// Initial Catalog - название файла базы данных
        /// Integrated Security - аутентификация пользователей со стороны Windows
        /// </summary>        /// <summary>
        /// Кэш данных в памяти ПК, в котором будут раполагаться DataTable для работы с БД
        /// </summary>
        public static DataSet dataSet = new DataSet();
        /// <summary>
        /// Кэш таблица, для вывода данных о ролях пользователей в системе
        /// </summary>
        private DataTable dtLicense = new DataTable("License");
        /// <summary>
        /// Кэш таблица, для вывода данных о сотрудниках
        /// </summary>
        private DataTable dtType_Organization = new DataTable("Type_Organization");
        /// <summary>
        /// Кэш таблица, для вывода данных о типе продуктов
        /// </summary>
        private DataTable dtBrand = new DataTable("Brand");
        /// <summary>
        /// Кэш таблица, для вывода данных о продуктах
        /// </summary>
        private DataTable dtDelivery_Points = new DataTable("Delivery_Points");
        /// <summary>
        /// Кэш таблица, для вывода данных об основных данных по операции продажи товара
        /// </summary
        private DataTable dtPost = new DataTable("Post");

        private DataTable dtCountry = new DataTable("Country");

        private DataTable dtCargo = new DataTable("Cargo");

        private DataTable dtModel = new DataTable("Model");

        private DataTable dtTransport = new DataTable("Transport");

        private DataTable dtCustomer = new DataTable("Customer");

        private DataTable dtCarrier = new DataTable("Carrier");

        private DataTable dtEmployee = new DataTable("Employee");

        private DataTable dtRoute_Sheet = new DataTable("Route_Sheet");

        private DataTable dtApplication = new DataTable("Application");

        private DataTable dtPoints_Route_Sheet = new DataTable("Points_Route_Sheet");

        private DataTable dtPost_Customer = new DataTable("Post_Customer");

        private DataTable dtCountry_Trasnport = new DataTable("Country_Trasnport");

        private DataTable dtPost_Employee = new DataTable("Post_Employee");

        private DataTable dtCargo_Application = new DataTable("Cargo_Application");

        private DataTable dtEmployee_License = new DataTable("Employee_License");
        /// </summary>
        public enum Function { select, insert, update, delete };
        /// <summary>
        /// Метод проверки подключения к источнику данных
        /// </summary>
        /// <returns>True - если подключение открыто и закрыто успешно., False - в строке подключения ошибка</returns>
        public bool connection_Checking()
        {
            try
            {
                //Попытка открыть подключение к базе данных
                connection.Open();
                dataSet.Tables.Clear();
                //Добавление в кэш данных, кэш таблицы "Роли сотрудников"
                dataSet.Tables.Add(dtLicense);
                //Добавление в кэш данных, кэш таблицы "Сотрудники"
                dataSet.Tables.Add(dtType_Organization);
                //Добавление в кэш данных, кэш таблицы "Тип продуктов"
                dataSet.Tables.Add(dtBrand);
                //Добавление в кэш данных, кэш таблицы "Продукты"
                dataSet.Tables.Add(dtDelivery_Points);
                //Добавление в кэш данных, кэш таблицы "Данные об операции продажи товара"
                dataSet.Tables.Add(dtPost);
                //Добавление в кэш данных, кэш таблицы "Данные об операции продажи товара"
                dataSet.Tables.Add(dtEmployee);
                //Добавление в кэш данных, кэш таблицы "Состав операции продажи товара"
                dataSet.Tables.Add(dtCountry);
                //Возвращение методу значения истинны
                dataSet.Tables.Add(dtCargo);
                //Возвращение методу значения истинны
                dataSet.Tables.Add(dtModel);
                //Возвращение методу значения истинны
                dataSet.Tables.Add(dtTransport);
                //Возвращение методу значения истинны
                dataSet.Tables.Add(dtCustomer);
                //Возвращение методу значения истинны
                dataSet.Tables.Add(dtCarrier);
                //Возвращение методу значения истинны
                
                //Возвращение методу значения истинны
                dataSet.Tables.Add(dtRoute_Sheet);
                //Возвращение методу значения истинны
                dataSet.Tables.Add(dtApplication);

                dataSet.Tables.Add(dtPoints_Route_Sheet);

                dataSet.Tables.Add(dtPost_Customer);

                dataSet.Tables.Add(dtCountry_Trasnport);

                dataSet.Tables.Add(dtPost_Employee);

                dataSet.Tables.Add(dtCargo_Application);

                

                dataSet.Tables.Add(dtEmployee_License);
                //Возвращение методу значения истинны
                return true;
            }
            //Объявление экземпляра класса, исключительных ситуация связанных с обработкой SQL запросов и работой с базами данных  
            catch (SqlException ex)
            {
                //Вывод сообщения об ошибке в случае ошибки в строке подключения
                MessageBox.Show(ex.Message, "Продажа товара");
                //Возвращение методу значения лжи
                return false;
            }
            finally
            {
                //Закрытие подключения, в независимости от результата
                connection.Close();
            }
        }

        /// <summary>
        /// Метод работы с любым запросом DML SQL
        /// </summary>
        /// <param name="SQLQuery">Обязательный запрос на выборку данных</param>
        /// <param name="TableName">Обязательная результирующая таблица</param>
        /// <param name="function">Вид манипуляции select, insert, update, delete</param>
        /// <param name="valueList">Коллекция передаваемых значений, если select то передать null</param>
        public void DataSetFill(string SQLQuery, string TableName, Function function, ArrayList valueList)
        {
            //Создание экземпляра класса Адаптера - включает в себя свойства и методыв по выборке, добавлению, изменению и удалению данных, в конструкторе данный запрос помещается в свойство SelectCommand
            SqlDataAdapter adapter = new SqlDataAdapter(SQLQuery, connection);
            //Создание экземпляра класса кэш таблицы для выборки объектов из базы данных
            DataTable table = new DataTable();
            //Создание экзмепляра класса обработчика SQL команд, для выборки данных об объектах базы данных
            SqlCommand command = new SqlCommand("", connection);
            try
            {
                connection.Open();
                //Отчистка, в кэше данных, у указанной таблицы, столбцов, для избежания аккамулирования столбцов
                //dataSet.Tables[TableName].Columns.Clear();
                //Отчистка, в кэше данных, у указанной таблицы, строк, для избежания аккаму лирования строк
                //dataSet.Tables[TableName].Rows.Clear();
                //Переключатель на выполнение одного из 4 действий
                switch (function)
                {
                    case Function.select:
                        //Заполнение, в кэше данных, указанной таблицы, запросом на выборку данных
                        adapter.Fill(dataSet.Tables[TableName]);
                        break;
                    case Function.insert:
                        //Формирование запроса на выборку объектов базы данных, а именно столбцов таблиц, с фильтрацией, где id таблицы равен введённому названию в метод и где поля не имеют свойство is_identity 1, то есть не являются PK
                        command.CommandText = string.Format("select name from sys.columns where object_id = (select object_id from sys.tables where name = '{0}') and is_identity <> 1", TableName);
                        //Заполнение кэш таблицы, реузльтатом выборки обектов из БД
                        table.Load(command.ExecuteReader());
                        //Формирование строки запроса на добавление данных в указанную таблицу
                        string insertquery = string.Format("insert into [dbo].[{0}] (", TableName);
                        //Организация цикла, для заполнения названия толбцов в соотвествии с запросом на выборку названий столбцов конкретной таблицы
                        for (int i = 0; i <= table.Rows.Count - 1; i++)
                        {
                            insertquery += string.Format(" [{0}]", table.Rows[i][0]);
                            //Проверка на то, является ли перечисленное поле не последнее в цикле, если да то ставим после названия столбца запятую 
                            if (i < table.Rows.Count - 1)
                                insertquery += ",";
                        }
                        //Дополнение строки запроса на выборку данных, командой values, которая раздеяет область описания столбцов и параметров
                        insertquery += ") values (";
                        //Организация цикла, для заполнения названия параметров к соотвествующим столбцам таблицы, куда будут добавлены данные
                        for (int i = 0; i <= table.Rows.Count - 1; i++)
                        {
                            //Дополнение запроса новыми параметрами
                            insertquery += string.Format(" @{0}", table.Rows[i][0]);
                            //Проверка на то, является ли перечисленный параметр не последнее в цикле, если да то ставим после названия параметра запятую 
                            if (i < table.Rows.Count - 1)
                                insertquery += ",";
                        }
                        //Дополнение запроса на добавление данных, закрывающей скобкой
                        insertquery += ")";
                        //Присвоение полученного запроса в свойство InsertCommand, через инициализацию нового обработчика SQL корманд
                        adapter.InsertCommand = new SqlCommand(insertquery);
                        //Инициализация свойству InsertCommand, свойству Connection экземпляра класса SQLConnection
                        adapter.InsertCommand.Connection = connection;
                        //Принудительная отчистка параметров у свойства InsertCommand, для избежания аккамулирования параметров
                        adapter.InsertCommand.Parameters.Clear();
                        //Организация цикла для присвоения полученного списка значений в параметры запроса на добавление данных
                        for (int i = 0; i <= table.Rows.Count - 1; i++)
                        {
                            //Добавление, в коллекцию свойства InsertCommand, значений в параметры по его названию
                            adapter.InsertCommand.Parameters.AddWithValue(string.Format("@{0}", table.Rows[i][0]), valueList[i]);
                        }
                        //Выполнение вложенного запроса на добавление данных
                        adapter.InsertCommand.ExecuteNonQuery();
                        //Перезапись кэш таблицы, с помощью запроса на выборку данных, для визуального обновления данных
                        adapter.Fill(dataSet.Tables[TableName]);
                        break;
                    case Function.update:
                        //Формирование запроса на выборку объектов базы данных, а именно столбцов таблиц, с фильтрацией, где id таблицы равен введённому названию в метод
                        command.CommandText = string.Format("select name from sys.columns where object_id = (select object_id from sys.tables where name = '{0}')", TableName);
                        //Заполнение кэш таблицы, реузльтатом выборки обектов из БД
                        table.Load(command.ExecuteReader());
                        //Формирование строки для изменения данных в указанной таблице базы данных
                        string updatequery = string.Format("update [dbo].[{0}] set", TableName);
                        //Организация цикла, для дополнения строки изменения базы данных, с учётом того, что цикл начинается не с 0-ой строки (PK), а с неключевых элементов данных
                        for (int i = 1; i <= table.Rows.Count - 1; i++)
                        {
                            //Дполнение запроса на изменение данных, строкой присвоения к полю таблицы, соответствующего параметра
                            updatequery += string.Format(" {0} = @{0}", table.Rows[i][0]);
                            //Проверка на то, является ли перечисленное поле не последнее в цикле, если да то ставим после названия поля запятую
                            if (i < table.Rows.Count - 1)
                                updatequery += ",";
                        }
                        //Дополнение запроса на изменение данных, условием и присвоением в поле первичного ключа соответствующего параметра
                        updatequery += string.Format(" where {0} = @{0}", table.Rows[0][0]);
                        //Присвоение полученного запроса в свойство UpdateCommand, через инициализацию нового обработчика SQL корманд
                        adapter.UpdateCommand = new SqlCommand(updatequery);
                        //Инициализация свойству UpdateCommand, свойству Connection экземпляра класса SQLConnection
                        adapter.UpdateCommand.Connection = connection;
                        //Принудительная отчистка параметров у свойства UpdateCommand, для избежания аккамулирования параметров
                        adapter.UpdateCommand.Parameters.Clear();
                        //Организация цикла для присвоения полученного списка значений в параметры запроса на изменение данных
                        for (int i = 0; i <= table.Rows.Count - 1; i++)
                        {
                            //Добавление, в коллекцию свойства UpdateCommand, значений в параметры по его названию
                            adapter.UpdateCommand.Parameters.AddWithValue(string.Format("@{0}", table.Rows[i][0]), valueList[i]);
                        }
                        //Выполнение вложенного запроса на изменение данных
                        adapter.UpdateCommand.ExecuteNonQuery();
                        //Перезапись кэш таблицы, с помощью запроса на выборку данных, для визуального обновления данных
                        adapter.Fill(dataSet.Tables[TableName]);
                        break;
                    case Function.delete:
                        //Формирование запроса на выборку объектов базы данных, а именно столбцов таблиц, с фильтрацией, где id таблицы равен введённому названию в метод и где поля имеют свойство is_identity 1, то есть являются PK
                        command.CommandText = string.Format("select name from sys.columns where object_id = (select object_id from sys.tables where name = '{0}') and is_identity = 1", TableName);
                        //Заполнение кэш таблицы, реузльтатом выборки обектов из БД
                        table.Load(command.ExecuteReader());
                        //Формирование строки запроса на удаление данных из указанной таблицы базы данных
                        string deletequery = string.Format("delete from [dbo].[{0}] where [{1}] = @{1}", TableName, table.Rows[0][0]);
                        //Присвоение полученного запроса в свойство DeleteCommand, через инициализацию нового обработчика SQL корманд
                        adapter.DeleteCommand = new SqlCommand(deletequery);
                        //Инициализация свойству DeleteCommand, свойству Connection экземпляра класса SQLConnection
                        adapter.DeleteCommand.Connection = connection;
                        //Принудительная отчистка параметров у свойства DeleteCommand, для избежания аккамулирования параметров
                        adapter.DeleteCommand.Parameters.Clear();
                        //Добавление в коллекцию свойства DeleteCommand значения с названием параметра для дальнейшего удаления данных
                        adapter.DeleteCommand.Parameters.AddWithValue(string.Format("@{0}", table.Rows[0][0]), valueList[0]);
                        //Выполнение вложенного запроса на удаление данных
                        adapter.DeleteCommand.ExecuteNonQuery();
                        //Перезапись кэш таблицы, с помощью запроса на выборку данных, для визуального обновления данных
                        adapter.Fill(dataSet.Tables[TableName]);
                        break;
                }
            }
            catch (SqlException ex)
            {
                //Вывод сообщения об ошибке при работе с базой данных
                MessageBox.Show(ex.Message, "Продажа товара");
                for (int i = 0; i <= valueList.Count - 1; i++)
                {
                    //Добавление, в коллекцию свойства InsertCommand, значений в параметры по его названию
                    MessageBox.Show(valueList[i].ToString(), "Продажа товара");
                }
  

            }
            finally
            {
                //Закрытие подключения к базе данных
                connection.Close();
            }
        }
    }
}
