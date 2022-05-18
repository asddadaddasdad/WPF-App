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
using System;
using System.Collections;       //Пространство имён, необходимое для обращение к классу ArrayList
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Data;              //Пространство имён для работы с кэш таблицами, строками, столбцами и данными//Пространство имён для работы с русурсами Microsoft Office Excel, с присвоением пространства имён в переменную 
using Microsoft.Win32;
using System.IO;

namespace WpfApp5
{
    /// <summary>
    /// Логика взаимодействия для Admin.xaml
    /// </summary>
    public partial class Admin : Window
    {
        public Admin()
        {
            InitializeComponent();
        }

        ArrayList values = new ArrayList();

        bool auth = false;

        OpenFileDialog openDilaog = new OpenFileDialog();

        string File_Path = string.Empty;

        string qrEmployee = "select [ID_Employee], [Name_Employee], [Surname_Employee], [Lastname_Employee], [Login_Employee], [Password_Employee], [SNILS], [FOMS], [Name_Carrier] from [dbo].[Employee] inner join [dbo].[Carrier] on [ID_Carrier] = [Carrier_ID]",
            qrApplication = "select [ID_Application], [Number_Application], [Date_Application], [Time_Application], [Status_Application], [Name_Customer], [Surname_Customer], [Lastname_Customer], [Number_Route_Sheet] from [dbo].[Application] inner join [dbo].[Customer] on [ID_Customer] = [Customer_ID] inner join [dbo].[Route_Sheet] on [ID_Route_Sheet] = [Route_Sheet_ID]",
            qrCargo = "select [ID_Transport], [Length_Transport], [Width_Transport], [Height_Transport], [Load_Copacity], [Copacity], [Number_Transport], [Name_Model] from [dbo].[Transport] inner join [dbo].[Model] on [ID_Model] = [Model_ID]",
            qrBrand = "select [ID_Brand], [Name_Brand] from [dbo].[Brand]",
            qrModel = "select [ID_Model], [Name_Model], [Name_Brand] from [dbo].[Model] inner join [dbo].[Brand] on [ID_Brand] = [Brand_ID]",
            qrTransport = "select [ID_Cargo], [Description_Cargo], [Weight_Cargo], [Length_Cargo], [Height_Cargo], [Width_Cargo] from [dbo].[Cargo]",
            qrCarrier = "select [ID_Carrier], [Name_Carrier], [Type_Organization_ID] from [dbo].[Carrier] inner join [dbo].[Type_Organization] on [ID_Type_Organization] = [Type_Organization_ID]",
            qrRoute_Sheet = "select [ID_Route_Sheet], [Number_Route_Sheet], [Date_Route_Sheet], [Time_Route_Sheet], [Name_Carrier], [Name_Model], [Number_Transport] from [dbo].[Route_Sheet] inner join [dbo].[Carrier] on [ID_Carrier] = [Carrier_ID] inner join [dbo].[Transport] on [ID_Transport] = [Transport_ID] inner join [dbo].[Model] on [ID_Model] = [Model_ID]",
            qrCustomer = "select [ID_Customer], [Name_Customer], [Surname_Customer], [Lastname_Customer], [Login_Customer], [Password_Customer], [TIN], [BIC], [OKPO], [Type_Name], [Name_Customer_Organization], [Organization_Address] from [dbo].[Customer] inner join [dbo].[Type_Organization] on [ID_Type_Organization] = [Type_Organization_ID]",
            qrPost = "select [ID_Post], [Name_Post] from [dbo].[Post]",
            qrPost_Customer = "select [ID_Post_Customer], [Surname_Customer], [Name_Customer], [Lastname_Customer], [Name_Post] from [dbo].[Post_Customer] inner join [dbo].[Customer] on [ID_Customer] = [Customer_ID] inner join [dbo].[Post] on [ID_Post] = [Post_ID]",
            qrPost_Employee = "select [ID_Post_Employee], [Surname_Employee], [Name_Employee], [Lastname_Employee], [Login_Employee], [Name_Post] from [dbo].[Post_Employee]inner join [dbo].[Employee] on [ID_Employee] = [Employee_ID] inner join[dbo].[Post] on[ID_Post] = [Post_ID]",
            qrType_Organization = "select [ID_Type_Organization], [Type_Name] from [dbo].[Type_Organization]",
            qrDelivery_Points = "select [ID_Delivery_Point], [Delivery_Address] from [dbo].[Delivery_Points]",
            qrPoints_Route_Sheet = "select [ID_Point_Route_Sheet], [Delivery_Address], [Number_Route_Sheet] from [dbo].[Points_Route_Sheet] inner join [dbo].[Delivery_Points] on [ID_Delivery_Point] = [Delivery_Point_ID] inner join [dbo].[Route_Sheet] on [ID_Route_Sheet] = [Route_Sheet_ID]",
            qrGGG = "select [ID_Post_Customer],[Post_ID] , [ID_Post], [Name_Post], [Customer_ID],[ID_Customer],[Name_Customer_Organization], [Organization_Address], [TIN],[BIC], [OKPO], [Login_Customer], [Password_Customer], [Name_Customer], [Surname_Customer], [Lastname_Customer] from [dbo].[Post_Customer] inner join [dbo].[Customer] on [ID_Customer] = [Customer_ID] inner join [dbo].[Post] on [ID_Post] = [Post_ID]";
        private void FileCreator()
        {
            //Если директории LogsSaver в директории с проектом не существует, то выполняется следующее условие
            if (!Directory.Exists(Directory.GetCurrentDirectory() + "\\LogsSaver"))
            {
                //Создание директории в папке с проектом
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\LogsSaver");
            }
            //Если файл Log.txt не существует, то выполняется условее
            if (!File.Exists((Directory.GetCurrentDirectory() + "\\LogsSaver\\Log.txt")))
            {
                //Открывается фаловый поток и на это же сторочке завершается, создаётся файл, если он существет от открывается
                using (FileStream fileStream = new FileStream((Directory.GetCurrentDirectory() + "\\LogsSaver\\Log.txt"), FileMode.OpenOrCreate)) { }
            }
        }
        //Метод записи в тектовый файл Log.txt, который находится в директории с проектом, который принимает строчную переменную для дальнейших манипуляций
        private void LogWriter(string bag)
        {
            //Открытие потока для записи текста в файла и в директорию с провектом, вторая переменная bool в объекте класс StreamWriter указвает на дозапись в файл
            using (StreamWriter write = new StreamWriter(((Directory.GetCurrentDirectory() + "\\LogsSaver\\Log.txt")), true))
            {
                //Обращение к объекту классу StreamWriter и вывод в текстовый файл с перевод на новую строку постоянно с записью принимаемой строки и сегодняшнего времени
                write.WriteLine($"[{DateTime.Now}] BadWork: {bag}");
            }
        }


        private void dgType_Organization_Loaded(object sender, RoutedEventArgs e)
        {
            Type_OrganizationFill();
        }

        private void Post_CustomerLoaded(object sender, RoutedEventArgs e)
        {
            Post_Customer1Fill();
        }

        public void Post_Customer1Fill()
        {
            DataSetClass dataSetClass = new DataSetClass();
            dataSetClass.DataSetFill(qrPost_Customer, "Post_Customer", DataSetClass.Function.select, null);
            cbPost_Customer.ItemsSource = DataSetClass.dataSet.Tables["Post_Customer"].DefaultView;
            //Присвоение в свойство "Путь к выбранному значению (PK)", название столбца первичного ключа, кэш таблицы "Тип продукта"
            cbPost_Customer.SelectedValuePath = DataSetClass.dataSet.Tables["Post_Customer"].Columns[0].ColumnName;
            //Присвоение в свойство "Путь к визуальному члену", название столбца "Название типа продукта", кэш таблицы "Тип продукта"
            cbPost_Customer.DisplayMemberPath = DataSetClass.dataSet.Tables["Post_Customer"].Columns[4].ColumnName;
        }
        private void Carrier2Loaded(object sender, RoutedEventArgs e)
        {
            Carrier2Fill();
        }
        public void Carrier2Fill()
        {
            DataSetClass dataSetClass = new DataSetClass();
            dataSetClass.DataSetFill(qrCarrier, "Carrier", DataSetClass.Function.select, null);
            сbCarrier1.ItemsSource = DataSetClass.dataSet.Tables["Carrier"].DefaultView;
            //Присвоение в свойство "Путь к выбранному значению (PK)", название столбца первичного ключа, кэш таблицы "Тип продукта"
            сbCarrier1.SelectedValuePath = DataSetClass.dataSet.Tables["Carrier"].Columns[0].ColumnName;
            //Присвоение в свойство "Путь к визуальному члену", название столбца "Название типа продукта", кэш таблицы "Тип продукта"
            сbCarrier1.DisplayMemberPath = DataSetClass.dataSet.Tables["Carrier"].Columns[1].ColumnName;
        }
        private void NoneLoaded(object sender, RoutedEventArgs e)
        {
            NoneFill();
        }
        public void NoneFill()
        {
            DataSetClass dataSetClass = new DataSetClass();
            dataSetClass.DataSetFill(qrCargo, "Cargo", DataSetClass.Function.select, null);
            сbNone1.ItemsSource = DataSetClass.dataSet.Tables["Cargo"].DefaultView;
            //Присвоение в свойство "Путь к выбранному значению (PK)", название столбца первичного ключа, кэш таблицы "Тип продукта"
            сbNone1.SelectedValuePath = DataSetClass.dataSet.Tables["Cargo"].Columns[0].ColumnName;
            //Присвоение в свойство "Путь к визуальному члену", название столбца "Название типа продукта", кэш таблицы "Тип продукта"
            сbNone1.DisplayMemberPath = DataSetClass.dataSet.Tables["Cargo"].Columns[6].ColumnName;
        }
        private void Delivery3_PointsLoaded(object sender, RoutedEventArgs e)
        {
            Delivery3_PointFill();
        }
        public void Delivery3_PointFill()
        {
            DataSetClass dataSetClass = new DataSetClass();
            dataSetClass.DataSetFill(qrRoute_Sheet, "Route_Sheet", DataSetClass.Function.select, null);
            cbdelivery_Points1.ItemsSource = DataSetClass.dataSet.Tables["Route_Sheet"].DefaultView;
            //Присвоение в свойство "Путь к выбранному значению (PK)", название столбца первичного ключа, кэш таблицы "Тип продукта"
            cbdelivery_Points1.SelectedValuePath = DataSetClass.dataSet.Tables["Route_Sheet"].Columns[0].ColumnName;
            //Присвоение в свойство "Путь к визуальному члену", название столбца "Название типа продукта", кэш таблицы "Тип продукта"
            cbdelivery_Points1.DisplayMemberPath = DataSetClass.dataSet.Tables["Route_Sheet"].Columns[1].ColumnName;
        }
        private void Delivery2_PointsLoaded(object sender, RoutedEventArgs e)
        {
            Delivery2_PointsFill();
        }
        public void Delivery2_PointsFill()
        {
            DataSetClass dataSetClass = new DataSetClass();
            dataSetClass.DataSetFill(qrDelivery_Points, "Delivery_Points", DataSetClass.Function.select, null);
            cbPoints_Route_Sheet.ItemsSource = DataSetClass.dataSet.Tables["Delivery_Points"].DefaultView;
            //Присвоение в свойство "Путь к выбранному значению (PK)", название столбца первичного ключа, кэш таблицы "Тип продукта"
            cbPoints_Route_Sheet.SelectedValuePath = DataSetClass.dataSet.Tables["Delivery_Points"].Columns[0].ColumnName;
            //Присвоение в свойство "Путь к визуальному члену", название столбца "Название типа продукта", кэш таблицы "Тип продукта"
            cbPoints_Route_Sheet.DisplayMemberPath = DataSetClass.dataSet.Tables["Delivery_Points"].Columns[1].ColumnName;
        }
        private void NumberListLoaded(object sender, RoutedEventArgs e)
        {
            NumberListFill();
        }
        public void NumberListFill()
        {
            DataSetClass dataSetClass = new DataSetClass();
            dataSetClass.DataSetFill(qrRoute_Sheet, "Route_Sheet", DataSetClass.Function.select, null);
            cbNumberList.ItemsSource = DataSetClass.dataSet.Tables["Route_Sheet"].DefaultView;
            //Присвоение в свойство "Путь к выбранному значению (PK)", название столбца первичного ключа, кэш таблицы "Тип продукта"
            cbNumberList.SelectedValuePath = DataSetClass.dataSet.Tables["Route_Sheet"].Columns[0].ColumnName;
            //Присвоение в свойство "Путь к визуальному члену", название столбца "Название типа продукта", кэш таблицы "Тип продукта"
            cbNumberList.DisplayMemberPath = DataSetClass.dataSet.Tables["Route_Sheet"].Columns[1].ColumnName;
        }
        private void Brand1Loaded(object sender, RoutedEventArgs e)
        {
            Brand1Fill();
        }
        public void Brand1Fill()
        {
            DataSetClass dataSetClass = new DataSetClass();
            dataSetClass.DataSetFill(qrBrand, "Brand", DataSetClass.Function.select, null);
            cbBrandName1.ItemsSource = DataSetClass.dataSet.Tables["Brand"].DefaultView;
            //Присвоение в свойство "Путь к выбранному значению (PK)", название столбца первичного ключа, кэш таблицы "Тип продукта"
            cbBrandName1.SelectedValuePath = DataSetClass.dataSet.Tables["Brand"].Columns[0].ColumnName;
            //Присвоение в свойство "Путь к визуальному члену", название столбца "Название типа продукта", кэш таблицы "Тип продукта"
            cbBrandName1.DisplayMemberPath = DataSetClass.dataSet.Tables["Brand"].Columns[1].ColumnName;
        }
        private void TimeLoaded(object sender, RoutedEventArgs e)
        {
            TimeFill();
        }
        public void TimeFill()
        {
            DataSetClass dataSetClass = new DataSetClass();
            dataSetClass.DataSetFill(qrRoute_Sheet, "Route_Sheet", DataSetClass.Function.select, null);
            cbTime.ItemsSource = DataSetClass.dataSet.Tables["Route_Sheet"].DefaultView;
            //Присвоение в свойство "Путь к выбранному значению (PK)", название столбца первичного ключа, кэш таблицы "Тип продукта"
            cbTime.SelectedValuePath = DataSetClass.dataSet.Tables["Route_Sheet"].Columns[0].ColumnName;
            //Присвоение в свойство "Путь к визуальному члену", название столбца "Название типа продукта", кэш таблицы "Тип продукта"
            cbTime.DisplayMemberPath = DataSetClass.dataSet.Tables["Route_Sheet"].Columns[3].ColumnName;
        }
        private void DataLoaded(object sender, RoutedEventArgs e)
        {
            DataFill();
        }
        public void DataFill()
        {
            DataSetClass dataSetClass = new DataSetClass();
            dataSetClass.DataSetFill(qrRoute_Sheet, "Route_Sheet", DataSetClass.Function.select, null);
            cbData.ItemsSource = DataSetClass.dataSet.Tables["Route_Sheet"].DefaultView;
            //Присвоение в свойство "Путь к выбранному значению (PK)", название столбца первичного ключа, кэш таблицы "Тип продукта"
            cbData.SelectedValuePath = DataSetClass.dataSet.Tables["Route_Sheet"].Columns[0].ColumnName;
            //Присвоение в свойство "Путь к визуальному члену", название столбца "Название типа продукта", кэш таблицы "Тип продукта"
            cbData.DisplayMemberPath = DataSetClass.dataSet.Tables["Route_Sheet"].Columns[2].ColumnName;
        }
        
        //private void MarkLoaded1(object sender, RoutedEventArgs e)
        //{
        //    Mark1Fill();
        //}
        //public void Mark1Fill()
        //{
        //    DataSetClass dataSetClass = new DataSetClass();
        //    dataSetClass.DataSetFill(qrBrand, "Brand", DataSetClass.Function.select, null);
        //    cbApplicationNumber.ItemsSource = DataSetClass.dataSet.Tables["Brand"].DefaultView;
        //    //Присвоение в свойство "Путь к выбранному значению (PK)", название столбца первичного ключа, кэш таблицы "Тип продукта"
        //    cbApplicationNumber.SelectedValuePath = DataSetClass.dataSet.Tables["Brand"].Columns[0].ColumnName;
        //    //Присвоение в свойство "Путь к визуальному члену", название столбца "Название типа продукта", кэш таблицы "Тип продукта"
        //    cbApplicationNumber.DisplayMemberPath = DataSetClass.dataSet.Tables["Brand"].Columns[1].ColumnName;
        //}
        private void NameLoaded(object sender, RoutedEventArgs e)
        {
            cbNameFill();
        }

        public void cbNameFill()
        {
            DataSetClass dataSetClass = new DataSetClass();
            dataSetClass.DataSetFill(qrPost_Customer, "Post_Customer", DataSetClass.Function.select, null);
            cbName.ItemsSource = DataSetClass.dataSet.Tables["Post_Customer"].DefaultView;
            //Присвоение в свойство "Путь к выбранному значению (PK)", название столбца первичного ключа, кэш таблицы "Тип продукта"
            cbName.SelectedValuePath = DataSetClass.dataSet.Tables["Post_Customer"].Columns[0].ColumnName;
            //Присвоение в свойство "Путь к визуальному члену", название столбца "Название типа продукта", кэш таблицы "Тип продукта"
            cbName.DisplayMemberPath = DataSetClass.dataSet.Tables["Post_Customer"].Columns[2].ColumnName;
        }

        private void LastNameLoaded(object sender, RoutedEventArgs e)
        {
            cbLastNameFill();
        }

        public void cbLastNameFill()
        {
            DataSetClass dataSetClass = new DataSetClass();
            dataSetClass.DataSetFill(qrPost_Customer, "Post_Customer", DataSetClass.Function.select, null);
            cbLastname.ItemsSource = DataSetClass.dataSet.Tables["Post_Customer"].DefaultView;
            //Присвоение в свойство "Путь к выбранному значению (PK)", название столбца первичного ключа, кэш таблицы "Тип продукта"
            cbLastname.SelectedValuePath = DataSetClass.dataSet.Tables["Post_Customer"].Columns[0].ColumnName;
            //Присвоение в свойство "Путь к визуальному члену", название столбца "Название типа продукта", кэш таблицы "Тип продукта"
            cbLastname.DisplayMemberPath = DataSetClass.dataSet.Tables["Post_Customer"].Columns[3].ColumnName;
        }
        private void dgPoints_Route_Sheet_Loaded(object sender, RoutedEventArgs e)
        {
            Points_Route_SheetFill();
        }

        private void Points_Route_SheetFill()
        {
            try
            {
                DataSetClass dataSetClass = new DataSetClass();
                dataSetClass.DataSetFill(qrPoints_Route_Sheet, "Points_Route_Sheet", DataSetClass.Function.select, null);
                dgPoints_Route_Sheet.ItemsSource = DataSetClass.dataSet.Tables["Points_Route_Sheet"].DefaultView;
                dgPoints_Route_Sheet.Columns[0].Visibility = Visibility.Hidden;
                dgPoints_Route_Sheet.Columns[1].Header = "Адрес Доставки";
                dgPoints_Route_Sheet.Columns[1].Header = "Номер Маршрутного Листа";
            }
            catch (Exception) { }
        }
        private void Mark_Loaded(object sender, RoutedEventArgs e)
        {
            MarkFill();
        }
        private void MarkFill()
        {
            try
            {
                DataSetClass dataSetClass = new DataSetClass();
                dataSetClass.DataSetFill(qrBrand, "Brand", DataSetClass.Function.select, null);
                dgMark.ItemsSource = DataSetClass.dataSet.Tables["Brand"].DefaultView;
                dgMark.Columns[0].Visibility = Visibility.Hidden;
                dgMark.Columns[1].Header = "Марка";
            }
            catch (Exception) { }
        }
        private void Type_OrganizationFill()
        {
            try
            {
                DataSetClass dataSetClass = new DataSetClass();
                dataSetClass.DataSetFill(qrType_Organization, "Type_Organization", DataSetClass.Function.select, null);
                dgTypeOrganization.ItemsSource = DataSetClass.dataSet.Tables["Type_Organization"].DefaultView;
                dgTypeOrganization.Columns[0].Visibility = Visibility.Hidden;
                dgTypeOrganization.Columns[1].Header = "Тип Организации";
            }
            catch (Exception) { }
        }

        private void btExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        

        private void SurnameLoaded(object sender, RoutedEventArgs e)
        {
            cbSurnameFill();
        }
        //NameOrganizationLoaded
        private void NameOrganizationLoaded(object sender, RoutedEventArgs e)
        {
            cbNameOrganizationFill();
        }
        public void cbNameOrganizationFill()
        {
            DataSetClass dataSetClass = new DataSetClass();
            dataSetClass.DataSetFill(qrCarrier, "Carrier", DataSetClass.Function.select, null);
            cbNameOrganization.ItemsSource = DataSetClass.dataSet.Tables["Carrier"].DefaultView;
            //Присвоение в свойство "Путь к выбранному значению (PK)", название столбца первичного ключа, кэш таблицы "Тип продукта"
            cbNameOrganization.SelectedValuePath = DataSetClass.dataSet.Tables["Carrier"].Columns[0].ColumnName;
            //Присвоение в свойство "Путь к визуальному члену", название столбца "Название типа продукта", кэш таблицы "Тип продукта"
            cbNameOrganization.DisplayMemberPath = DataSetClass.dataSet.Tables["Carrier"].Columns[1].ColumnName;
        }
        public void cbSurnameFill()
        {
            DataSetClass dataSetClass = new DataSetClass();
            dataSetClass.DataSetFill(qrPost_Customer, "Post_Customer", DataSetClass.Function.select, null);
            cbSurname.ItemsSource = DataSetClass.dataSet.Tables["Post_Customer"].DefaultView;
            //Присвоение в свойство "Путь к выбранному значению (PK)", название столбца первичного ключа, кэш таблицы "Тип продукта"
            cbSurname.SelectedValuePath = DataSetClass.dataSet.Tables["Post_Customer"].Columns[0].ColumnName;
            //Присвоение в свойство "Путь к визуальному члену", название столбца "Название типа продукта", кэш таблицы "Тип продукта"
            cbSurname.DisplayMemberPath = DataSetClass.dataSet.Tables["Post_Customer"].Columns[1].ColumnName;
        }
        private void Model_Loaded(object sender, RoutedEventArgs e)
        {
            cbModelFill();
        }

        private void Model2_Loaded(object sender, RoutedEventArgs e)
        {
            Model2Fill();
        }

        private void Model2Fill()
        {
            try
            {
                DataSetClass dataSetClass = new DataSetClass();
                dataSetClass.DataSetFill(qrModel, "Model", DataSetClass.Function.select, null);
                dgModel.ItemsSource = DataSetClass.dataSet.Tables["Model"].DefaultView;
                dgModel.Columns[0].Visibility = Visibility.Hidden;
                dgModel.Columns[1].Header = "Модель";
                dgModel.Columns[2].Header = "Марка";
            }
            catch (Exception) { }
        }
        private void dgDriver_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void dgCargo_Loaded(object sender, RoutedEventArgs e)
        {
            CargoFill();
        }

        private void dgDelivery_Points_Loaded(object sender, RoutedEventArgs e)
        {
            Delivery_Pointsill();
        }

        private void Delivery_Pointsill()
        {
            try
            {
                DataSetClass dataSetClass = new DataSetClass();
                dataSetClass.DataSetFill(qrDelivery_Points, "Delivery_Points", DataSetClass.Function.select, null);
                dgDelivery_Points.ItemsSource = DataSetClass.dataSet.Tables["Delivery_Points"].DefaultView;
                dgDelivery_Points.Columns[0].Visibility = Visibility.Hidden;
                dgDelivery_Points.Columns[1].Header = "Адрес Точки Доставки";
            }
            catch (Exception) { }
        }
        private void dgApplication_Loaded(object sender, RoutedEventArgs e)
        {
            ApplicationFill();
        }

        private void dgRoute_Sheet_Loaded(object sender, RoutedEventArgs e)
        {
            Route_SheetFill();
        }

        private void dgCustomer_Loaded(object sender, RoutedEventArgs e)
        {
            CustomerFill();
        }

        private void dgPost_Customer_Loaded(object sender, RoutedEventArgs e)
        {
            Post_CustomerFill();
        }

        private void Post_CustomerFill()
        {
            try
            {
                DataSetClass dataSetClass = new DataSetClass();
                dataSetClass.DataSetFill(qrPost_Customer, "Post_Customer", DataSetClass.Function.select, null);
                dgPost_Customer.ItemsSource = DataSetClass.dataSet.Tables["Post_Customer"].DefaultView;
                dgPost_Customer.Columns[0].Visibility = Visibility.Hidden;
                dgPost_Customer.Columns[1].Visibility = Visibility.Hidden;
                dgPost_Customer.Columns[2].Visibility = Visibility.Hidden;
                dgPost_Customer.Columns[4].Visibility = Visibility.Hidden;
                dgPost_Customer.Columns[5].Visibility = Visibility.Hidden;
                //dgPost_Customer.Columns[1].Header = "Фамилия";
                //dgPost_Customer.Columns[2].Header = "Имя";
                //dgPost_Customer.Columns[3].Header = "Отчество";
                //dgPost_Customer.Columns[4].Header = "Должность";
            }
            catch (Exception) { }
        }

        private void dgPost_Employee_Loaded(object sender, RoutedEventArgs e)
        {
            Post_EmployeeFill();
        }

        private void Post_EmployeeFill()
        {
            try
            {
                DataSetClass dataSetClass = new DataSetClass();
                dataSetClass.DataSetFill(qrPost_Employee, "Post_Employee", DataSetClass.Function.select, null);
                dgPost_Employee.ItemsSource = DataSetClass.dataSet.Tables["Post_Employee"].DefaultView;
                dgPost_Employee.Columns[0].Visibility = Visibility.Hidden;
                dgPost_Employee.Columns[1].Header = "Фамилия";
                dgPost_Employee.Columns[2].Header = "Имя";
                dgPost_Employee.Columns[3].Header = "Отчество";
                dgPost_Employee.Columns[4].Header = "Логин";
                dgPost_Employee.Columns[5].Header = "Должность";
            }
            catch (Exception) { }
        }
        private void CustomerFill()
        {
            try
            {
                DataSetClass dataSetClass = new DataSetClass();
                dataSetClass.DataSetFill(qrCustomer, "Customer", DataSetClass.Function.select, null);
                dgCustomer.ItemsSource = DataSetClass.dataSet.Tables["Customer"].DefaultView;
                dgCustomer.Columns[0].Visibility = Visibility.Hidden;
                dgCustomer.Columns[1].Header = "Фамилия";
                dgCustomer.Columns[2].Header = "Имя";
                dgCustomer.Columns[3].Header = "Отчество";
                dgCustomer.Columns[4].Header = "Логин";
                dgCustomer.Columns[5].Header = "Пароль";
                dgCustomer.Columns[6].Header = "ИИН";
                dgCustomer.Columns[7].Header = "БИК";
                dgCustomer.Columns[8].Header = "ОКПО";
                dgCustomer.Columns[9].Header = "Тип организации";
                dgCustomer.Columns[10].Header = "Название Организации";
            }
            catch (Exception) { }
        }
        private void Route_SheetFill()
        {
            try
            {
                DataSetClass dataSetClass = new DataSetClass();
                dataSetClass.DataSetFill(qrRoute_Sheet, "Route_Sheet", DataSetClass.Function.select, null);
                dgRoute_Sheet.ItemsSource = DataSetClass.dataSet.Tables["Route_Sheet"].DefaultView;
                dgRoute_Sheet.Columns[0].Visibility = Visibility.Hidden;
                dgRoute_Sheet.Columns[1].Header = "Номер";
                dgRoute_Sheet.Columns[2].Header = "Дата";
                dgRoute_Sheet.Columns[3].Header = "Время";
                dgRoute_Sheet.Columns[4].Header = "Перевозчик";
                dgRoute_Sheet.Columns[5].Header = "";
            }
            catch (Exception) { }
        }
        private void dgCarrier_Loaded(object sender, RoutedEventArgs e)
        {
            CarrierFill();
        }
        private void dgEmployee_Loaded(object sender, RoutedEventArgs e)
        {
            EmployeeFill();
        }

        private void dgTransport_Loaded(object sender, RoutedEventArgs e)
        {
            TransportFill();
        }

        private void dgPost_Loaded(object sender, RoutedEventArgs e)
        {
            PostFill();
        }

        private void btEmployeeInsert12_Click(object sender, RoutedEventArgs e)
        {
            values.Clear();
            if (!string.IsNullOrEmpty(tbTypeOrganization1.Text))
            {
                values.Add(tbTypeOrganization1.Text);

                DataSetClass dataSetClass = new DataSetClass();
                dataSetClass.DataSetFill(qrType_Organization, "Type_Organization", DataSetClass.Function.insert, values);
                tbTypeOrganization1.Text = string.Empty;
                MessageBox.Show("HOROSHO");
            }
            else
            {
                MessageBox.Show("Введите Значение");
                LogWriter("Не указано значение");
            }
        }

        private void btEmployeeInsert5_Click(object sender, RoutedEventArgs e)
        {
            values.Clear();
            if (!string.IsNullOrEmpty(tbWDescribtion.Text))
            {
                values.Add(tbWDescribtion.Text);
                if (!string.IsNullOrEmpty(tbWeidth.Text))
                {
                    values.Add(tbWeidth.Text);
                    if (!string.IsNullOrEmpty(tbLenght.Text))
                    {
                        values.Add(tbLenght.Text);
                        if (!string.IsNullOrEmpty(tbHeight.Text))
                        {
                            values.Add(tbHeight.Text);
                            if (!string.IsNullOrEmpty(tbWidth.Text))
                            {
                                values.Add(tbWidth.Text);

                                DataSetClass dataSetClass = new DataSetClass();
                                dataSetClass.DataSetFill(qrTransport, "Cargo", DataSetClass.Function.insert, values);
                                
                                tbWDescribtion.Text = string.Empty;
                                tbWeidth.Text = string.Empty;
                                tbLenght.Text = string.Empty;
                                tbHeight.Text = string.Empty;
                                tbWidth.Text = string.Empty;
                                MessageBox.Show("HOROSHO");
                            }
                            else
                            {
                                MessageBox.Show("Введите ширину");
                                LogWriter("Не вписана ширина");

                            }
                        }
                        else
                        {
                            MessageBox.Show("Введите высоту");
                            LogWriter("Не вписана высота");

                        }


                    }
                    else
                    {
                        MessageBox.Show("Введите длину");
                        LogWriter("Не вписана длина");

                    }
                }
                else
                {
                    MessageBox.Show("Введите вес");
                    LogWriter("Не вписан вес");

                }
            }
            else
            {
                MessageBox.Show("Нужно заполнить поле описание!");
                LogWriter("Не заполнено поле записание");

            }

        }

        private void btEmployeeInsert7_Click(object sender, RoutedEventArgs e)
        {
            values.Clear();
            if (!string.IsNullOrEmpty(tbPost.Text))
            {
                values.Add(tbPost.Text);
                //Создание экземпляра класса работы с базой данных
                DataSetClass dataSetClass = new DataSetClass();
                //Вызов метода работы с запросами базы данных, с передачей аргументов: строковой переменной с запросом на выборку данных из таблицы "Тип продуктов",
                // название кэш таблицы, иструкции к алгоритму формирования запроса на добавление данных, не типизированный список с входными данными в запрос
                dataSetClass.DataSetFill(qrPost, "Post", DataSetClass.Function.insert, values);
                //Отчистка поля ввода
                tbPost.Text = string.Empty;
                MessageBox.Show("HOROSHO");
            }
            else
            {
                MessageBox.Show("Впешите данные");
                LogWriter("Не вписаны данные");
            }
        }

        private void btEmployeeInsert6_Click(object sender, RoutedEventArgs e)
        {
            values.Clear();
            if (!string.IsNullOrEmpty(tbName1Organization.Text))
            {
                values.Add(tbName1Organization.Text);
                if (!string.IsNullOrEmpty(tbTypeOrganization.Text))
                {
                    values.Add(tbTypeOrganization.Text);
                    DataSetClass dataSetClass = new DataSetClass();
                    dataSetClass.DataSetFill(qrCarrier, "Carrier", DataSetClass.Function.insert, values);
                    tbName1Organization.Text = string.Empty;
                    tbTypeOrganization.Text = string.Empty;
                    MessageBox.Show("HOROSHO");
                }
                else
                {
                    MessageBox.Show("Введите тип организации");
                    LogWriter("Не ввидено название организации");
                }
            }
            else
            {
                MessageBox.Show("Введите значение");
                LogWriter("Не ввидено название организации");
            }
        }

        private void btEmployeeInsert2_Click(object sender, RoutedEventArgs e)
        {
            values.Clear();
            if(!string.IsNullOrEmpty(tbApplicationNumber.Text))
            {
                values.Add(tbApplicationNumber.Text);
                if (cbData.SelectedValue != null)
                {
                    values.Add(cbData.Text);
                    if (cbTime.SelectedValue != null)
                    {
                        values.Add(cbTime.Text);
                        if (!string.IsNullOrEmpty(tbStatus.Text))
                        {
                            values.Add(tbStatus.Text);
                            if (!string.IsNullOrEmpty(tbFirstName2.Text))
                            {
                                values.Add(tbFirstName2.Text);
                                if (!string.IsNullOrEmpty(tbSecondName2.Text))
                                {
                                    values.Add(tbSecondName2.Text);
                                    if (!string.IsNullOrEmpty(tbMiddleName2.Text))
                                    {
                                        values.Add(tbMiddleName2.Text);
                                        if (cbNumberList.SelectedValue != null)
                                        {
                                            values.Add(cbNumberList.Text);
                                            DataSetClass dataSetClass = new DataSetClass();
                                            dataSetClass.DataSetFill(qrApplication, "Application", DataSetClass.Function.update, values);


                                            tbApplicationNumber.Text = string.Empty;
                                            tbStatus.Text = string.Empty;
                                            tbFirstName2.Text = string.Empty;
                                            tbSecondName2.Text = string.Empty;
                                            tbMiddleName2.Text = string.Empty;
                                            MessageBox.Show("HOROSHO");
                                        }
                                        else
                                        {
                                            MessageBox.Show("Введите Номер маршрутного листа");
                                            cbNumberList.Focus();
                                            LogWriter("Не выбран номер маршрутного листа");

                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Введите Отчество");
                                        tbMiddleName2.Focus();
                                        LogWriter("Не ввидено отчество");

                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Введите имя");
                                    tbSecondName2.Focus();
                                    LogWriter("Не ввидено имя");

                                }
                            }
                            else
                            {
                                MessageBox.Show("Введите  Фамилию");
                                tbStatus.Focus();
                                LogWriter("Не ввидена фамилия");

                            }
                        }
                        else
                        {
                            MessageBox.Show("Выберите статус");
                            tbStatus.Focus();
                            LogWriter("Не выбран статус");

                        }
                    }
                    else
                    {
                        MessageBox.Show("Выберите время");
                        cbTime.Focus();
                        LogWriter("Не выбрано время");

                    }
                }
                else
                {
                    MessageBox.Show("Введите дату");
                    cbData.Focus();
                    LogWriter("Не выбрана дата высота");

                }
            }
            else
            {
                MessageBox.Show("Введите Номер заявки");
                tbApplicationNumber.Focus();
                LogWriter("Не ввиден номер заявки");

            }
        }

        private void btEmployeeInsert10_Click(object sender, RoutedEventArgs e)
        {
            values.Clear();
            if (cbSurname.SelectedValue != null)
            {
                values.Add(cbSurname.SelectedValue);

                if (cbName.SelectedValue != null)
                {
                    values.Add(cbName.SelectedValue);
                    
                    if (cbLastname.SelectedValue != null)
                    {
                        values.Add(cbLastname.SelectedValue);

                        if (cbPost_Customer.SelectedValue != null)
                        {
                            values.Add(cbPost_Customer.SelectedValue);
                            DataSetClass dataSetClass = new DataSetClass();
                            dataSetClass.DataSetFill(qrPost_Customer, "Post_Customer", DataSetClass.Function.insert, values);
                        }
                        else
                        {
                            MessageBox.Show("Выберите Должность");
                            cbPost_Customer.Focus();
                            LogWriter("Не выбрана Должность");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Выберите Отчество");
                        cbLastname.Focus();
                        LogWriter("Не выбрано Отчество");
                    }
                }
                else
                {
                    MessageBox.Show("Выберите Имя");
                    cbName.Focus();
                    LogWriter("Не выбрано имя");
                }
            }
            else
            {
                MessageBox.Show("Выберите Фамилию");
                cbSurname.Focus();
                LogWriter("Не выбрана Фамилия");
            }
        }

        private void btBrandInsert_Click(object sender, RoutedEventArgs e)
        {
            values.Clear();
            if (!string.IsNullOrEmpty(tbBrandName.Text))
            {
                values.Add(tbBrandName.Text);
                DataSetClass dataSetClass = new DataSetClass();
                dataSetClass.DataSetFill(qrBrand, "Brand", DataSetClass.Function.insert, values);
                tbBrandName.Text = string.Empty;
                MessageBox.Show("HOROSHO");
            }
            else
            {
                MessageBox.Show("Введите название марки");
                tbBrandName.Focus();
                LogWriter("Не ввидено название марки");
            }
        }

        private void btModelInsert_Click(object sender, RoutedEventArgs e)
        {
            values.Clear();
            if (!string.IsNullOrEmpty(tbModelName.Text))
            {
                values.Add(tbModelName.Text);
                if (cbBrandName1.SelectedValue != null)
                {
                    values.Add(cbBrandName1.SelectedValue);
                    DataSetClass dataSetClass = new DataSetClass();
                    dataSetClass.DataSetFill(qrModel, "Model", DataSetClass.Function.insert, values);
                }
                else
                {
                    MessageBox.Show("Выберите марку");
                    LogWriter("Не выбрана марка");

                }
            }
            else
            {
                MessageBox.Show("Введите модель");
                LogWriter("Не выбрана модель");

            }

        }

        private void btProductUpdate_Click(object sender, RoutedEventArgs e)
        {
            DataRowView rowView = (DataRowView)dgCargo.SelectedItems[0];
            values.Clear();
            if (rowView[0] != null)
            {
                values.Add(rowView[0]);

                if (!string.IsNullOrEmpty(tbLenght2.Text))
                {
                    values.Add(tbLenght2.Text);

                    if (!string.IsNullOrEmpty(tbWidht2.Text))
                    {
                        values.Add(tbWidht2.Text);

                        if (!string.IsNullOrEmpty(tbHeight2.Text))
                        {
                            values.Add(tbHeight2.Text);
                            if (!string.IsNullOrEmpty(tbCarrying.Text))
                            {
                                values.Add(tbCarrying.Text);
                                if (!string.IsNullOrEmpty(tbCapacity.Text))
                                {
                                    values.Add(tbCapacity.Text);
                                    if (!string.IsNullOrEmpty(tbNumber.Text))
                                    {
                                        values.Add(tbNumber.Text);
                                        if (cbModel.SelectedValue != null)
                                        {
                                            values.Add(cbModel.SelectedValue);
                                            DataSetClass dataSetClass = new DataSetClass();
                                            //Вызов метода работы с запросами базы данных, с передачей аргументов: строковой переменной с запросом на выборку данных из таблицы "Расписание",
                                            // название кэш таблицы, иструкции к алгоритму формирования запроса на изменение данных, не типизированный список с входными данными в запрос
                                            dataSetClass.DataSetFill(qrCargo, "Cargo", DataSetClass.Function.update, values);
                                            tbLenght2.Text = string.Empty;
                                            tbWidht2.Text = string.Empty;
                                            tbHeight2.Text = string.Empty;
                                            tbCarrying.Text = string.Empty;
                                            tbCapacity.Text = string.Empty;
                                            tbNumber.Text = string.Empty;
                                            MessageBox.Show("HOROSHO");
                                        }
                                        else
                                        {
                                            MessageBox.Show("Выберите модель продукта!");
                                            //Перевод курсора и фокуса на указанный визуальный элемент управления
                                            cbModel.Focus();
                                            LogWriter("Не выбрана модель");
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Выберите бренд продукта!");
                                        tbNumber.Focus();
                                        LogWriter("Не выбран номер");
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Выберите вместимость!");
                                    tbCapacity.Focus();
                                    LogWriter("Не выбрана вместимость");
                                }
                            }
                            else
                            {
                                MessageBox.Show("Выберите грузоподьемность!");
                                tbCarrying.Focus();
                                LogWriter("Не выбрана грузоподъемность");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Выберите высоту!");
                            tbHeight2.Focus();
                            LogWriter("Не выбрана высота");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Выберите ширину!");
                        tbWidht2.Focus();
                        LogWriter("Не выбрана ширина");
                    }
                }
                else
                {
                    MessageBox.Show("Выберите длину!");
                    tbLenght2.Focus();
                    LogWriter("Не выбрана длина");
                }
            }
            else
            {
                MessageBox.Show("Выберите транспорт который хотите изменить!");
                dgCargo.Focus();
                LogWriter("Не выбран транспорт который хотите изменить");
            }
        }

        private void Button_Click_23(object sender, RoutedEventArgs e)
        {
            Color color = Color.FromRgb((byte)BackR.Value, (byte)BackG.Value, (byte)BackB.Value);
            colorMain.Background = new SolidColorBrush(color);
        }

        private void Button_Click_24(object sender, RoutedEventArgs e)
        {
            Color color = Color.FromRgb((byte)BackR.Value, (byte)BackG.Value, (byte)BackB.Value);
            dgCargo.Background = new SolidColorBrush(color);
        }

        private void Button_Click_25(object sender, RoutedEventArgs e)
        {
            Color color = Color.FromRgb((byte)BackR.Value, (byte)BackG.Value, (byte)BackB.Value);
            dgModel.Background = new SolidColorBrush(color);
        }

        private void Button_Click_26(object sender, RoutedEventArgs e)
        {
            Color color = Color.FromRgb((byte)BackR.Value, (byte)BackG.Value, (byte)BackB.Value);
            dgMark.Background = new SolidColorBrush(color);
        }

        private void Button_Click_27(object sender, RoutedEventArgs e)
        {
            Color color = Color.FromRgb((byte)BackR.Value, (byte)BackG.Value, (byte)BackB.Value);
            dgRoute_Sheet.Background = new SolidColorBrush(color);
        }

        private void Button_Click_28(object sender, RoutedEventArgs e)
        {
            Color color = Color.FromRgb((byte)BackR.Value, (byte)BackG.Value, (byte)BackB.Value);
            dgEmployee.Background = new SolidColorBrush(color);
        }

        private void Button_Click_33(object sender, RoutedEventArgs e)
        {
            Color color = Color.FromRgb((byte)BackR.Value, (byte)BackG.Value, (byte)BackB.Value);
            dgApplication.Background = new SolidColorBrush(color);
        }

        private void Button_Click_34(object sender, RoutedEventArgs e)
        {
            Color color = Color.FromRgb((byte)BackR.Value, (byte)BackG.Value, (byte)BackB.Value);
            dgTransport.Background = new SolidColorBrush(color);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Color color = Color.FromRgb((byte)BackR.Value, (byte)BackG.Value, (byte)BackB.Value);
            dgCarrier.Background = new SolidColorBrush(color);
        }

        private void Button_Click_22(object sender, RoutedEventArgs e)
        {
            Color color = Color.FromRgb((byte)BackR.Value, (byte)BackG.Value, (byte)BackB.Value);
            recc.Fill = new SolidColorBrush(color);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Color color = Color.FromRgb((byte)BackR.Value, (byte)BackG.Value, (byte)BackB.Value);
            dgPost.Background = new SolidColorBrush(color);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Color color = Color.FromRgb((byte)BackR.Value, (byte)BackG.Value, (byte)BackB.Value);
            dgPost_Customer.Background = new SolidColorBrush(color);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Color color = Color.FromRgb((byte)BackR.Value, (byte)BackG.Value, (byte)BackB.Value);
            dgPost_Employee.Background = new SolidColorBrush(color);
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Color color = Color.FromRgb((byte)BackR.Value, (byte)BackG.Value, (byte)BackB.Value);
            dgCustomer.Background = new SolidColorBrush(color);
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            Color color = Color.FromRgb((byte)BackR.Value, (byte)BackG.Value, (byte)BackB.Value);
            dgTypeOrganization.Background = new SolidColorBrush(color);
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            Color color = Color.FromRgb((byte)BackR.Value, (byte)BackG.Value, (byte)BackB.Value);
            dgDelivery_Points.Background = new SolidColorBrush(color);
        }

        private void btImport_Click(object sender, RoutedEventArgs e)
        {
            //SaveFileDialog saveFile = new SaveFileDialog();
            //saveFile.DefaultExt = ".xls";
            //saveFile.Filter = "Excel Files|*.xls;*.xlsx;*.xlsn";

            //if (saveFile.ShowDialog() == true)
            //{
            //    try
            //    {
            //        DateTime dateTime = new DateTime();
            //        dgCargo.SelectAllCells();
            //        dgCargo.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
            //        ApplicationCommands.Copy.Execute(null, dgCargo);
            //        String res1 = (string)Clipboard.GetData(DataFormats.Html);
            //        String res = (string)Clipboard.GetData(DataFormats.Text);

            //        System.IO.StreamWriter file = new System.IO.StreamWriter(saveFile.FileName, true, Encoding.GetEncoding(866));
            //        file.WriteLine(res1.Replace("<TABLE>", String.Format($"<TABLE><TR><TD>{DateTime.Now}, Ataniyazov Alikhan <TD></TR>", dgCargo.Columns.Count)));
            //        Clipboard.SetText(res1, TextDataFormat.Html);
            //        file.Close();
            //        MessageBox.Show("Импорт завершен");
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.Message);
            //    }
            //}
        }

        private void btImport2_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.DefaultExt = ".xls";
            saveFile.Filter = "Excel Files|*.xls;*.xlsx;*.xlsn";

            if (saveFile.ShowDialog() == true)
            {
                try
                {
                    dgEmployee.SelectAllCells();
                    dgEmployee.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
                    ApplicationCommands.Copy.Execute(null, dgEmployee);
                    String res = (string)Clipboard.GetData(DataFormats.Text);

                    System.IO.StreamWriter file = new System.IO.StreamWriter(saveFile.FileName, true, Encoding.GetEncoding(1251));
                    file.WriteLine(res.Replace("<TABLE>", String.Format($"<TABLE><TR><TD>{DateTime.Now}, Ataniyazov Alikhan <TD></TR>", dgEmployee.Columns.Count)));
                    Clipboard.SetText(res, TextDataFormat.Html);
                    file.Close();
                    MessageBox.Show("Импорт завершен");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }

        private void btBrandUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                values.Clear();
                //Создание экземпляра класса, кэш представление строки данных, с присвоением, с явным преобразованием, выбранной строки таблицы
                DataRowView rowView = (DataRowView)dgMark.SelectedItems[0];
                //Принудительная отчистка коллекции не типизированного списка вводимых параметров, для избежания аккамулирования значений

                //Условие с проверкой того, не является ли, первый столбец кэш таблицы пустым, который отвечет за значение первичного ключа таблицы "Продукты"
                if (rowView[0] != null)
                {
                    values.Add(rowView[0]);

                    if (!string.IsNullOrEmpty(tbBrandName.Text))
                    {
                        values.Add(tbBrandName.Text);
                        DataSetClass dataSetClass = new DataSetClass();
                        //Вызов метода работы с запросами базы данных, с передачей аргументов: строковой переменной с запросом на выборку данных из таблицы "Продукты",
                        // название кэш таблицы, иструкции к алгоритму формирования запроса на изменение данных, не типизированный список с входными данными в запрос
                        dataSetClass.DataSetFill(qrBrand, "Brand", DataSetClass.Function.update, values);
                        tbBrandName.Text = string.Empty;
                        MessageBox.Show("HOROSHO");
                    }
                    else
                    {
                        MessageBox.Show("Выберите марку, который хотите изменить");
                        //Перевод курсора и фокуса на указанный визуальный элемент управления
                        dgMark.Focus();
                        LogWriter("Не указана марка");
                    }
                }
                else
                {
                    //Вывод сообщения об ошибке, что запись не выбрана в элемента управления
                    MessageBox.Show("Выберите значение, который хотите изменить");
                    //Перевод курсора и фокуса на указанный визуальный элемент управления
                    dgMark.Focus();
                    LogWriter("Не выбрано значение");
                }
            }
            catch
            {
                MessageBox.Show("Выберите бренд, который хотите изменить!");
                dgMark.Focus();
                LogWriter("Не выбран бренд");
            }
        }

        private void btEmployeeUpdate5_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                values.Clear();
                //Создание экземпляра класса, кэш представление строки данных, с присвоением, с явным преобразованием, выбранной строки таблицы
                DataRowView rowView = (DataRowView)dgTransport.SelectedItems[0];
                if (rowView[0] != null)
                {
                    values.Add(rowView[0]);
                    if (!string.IsNullOrEmpty(tbWDescribtion.Text))
                    {
                        values.Add(tbWDescribtion.Text);
                        if (!string.IsNullOrEmpty(tbWeidth.Text))
                        {
                            values.Add(tbWeidth.Text);
                            if (!string.IsNullOrEmpty(tbLenght.Text))
                            {
                                values.Add(tbLenght.Text);
                                if (!string.IsNullOrEmpty(tbHeight.Text))
                                {
                                    values.Add(tbHeight.Text);
                                    if (!string.IsNullOrEmpty(tbWidth.Text))
                                    {
                                        values.Add(tbWidth.Text);
                                        DataSetClass dataSetClass = new DataSetClass();
                                        //Вызов метода работы с запросами базы данных, с передачей аргументов: строковой переменной с запросом на выборку данных из таблицы "Продукты",
                                        // название кэш таблицы, иструкции к алгоритму формирования запроса на изменение данных, не типизированный список с входными данными в запрос
                                        dataSetClass.DataSetFill(qrTransport, "Cargo", DataSetClass.Function.update, values);
                                        tbWDescribtion.Text = string.Empty;
                                        tbWeidth.Text = string.Empty;
                                        tbLenght.Text = string.Empty;
                                        tbHeight.Text = string.Empty;
                                        tbWidth.Text = string.Empty;
                                        MessageBox.Show("HOROSHO");
                                    }
                                    else
                                    {
                                        MessageBox.Show("Выберите ширину, которую хотите изменить!");
                                        dgTransport.Focus();
                                        LogWriter("Не указана ширина");
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Выберите высоту, которую хотите изменить!");
                                    dgTransport.Focus();
                                    LogWriter("Не указана высота");
                                }
                            }
                            else
                            {
                                MessageBox.Show("Выберите длину, которую хотите изменить!");
                                dgTransport.Focus();
                                LogWriter("Не указана длина");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Выберите вес, который хотите изменить!");
                            dgTransport.Focus();
                            LogWriter("Не указан вес");
                        }

                    }
                    else
                    {
                        MessageBox.Show("Выберите описание, который хотите изменить!");
                        dgTransport.Focus();
                        LogWriter("Не выбрано описание");
                    }
                }
                else
                {
                    MessageBox.Show("Выберите значение, который хотите изменить!");
                    dgTransport.Focus();
                    LogWriter("Не выбрано значение");
                }
            }
            catch
            {
                MessageBox.Show("Выберите груз, который хотите изменить!");
                dgTransport.Focus();
                LogWriter("Не выбран груз");
            }
        }

        private void btEmployeeUpdate6_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                values.Clear();
                //Создание экземпляра класса, кэш представление строки данных, с присвоением, с явным преобразованием, выбранной строки таблицы
                DataRowView rowView = (DataRowView)dgCarrier.SelectedItems[0];
                if (rowView[0] != null)
                {
                    values.Add(rowView[0]);
                    if (!string.IsNullOrEmpty(tbName1Organization.Text))
                    {
                        values.Add(tbName1Organization.Text);
                        if (!string.IsNullOrEmpty(tbTypeOrganization.Text))
                        {
                            values.Add(tbTypeOrganization.Text);
                            DataSetClass dataSetClass = new DataSetClass();
                            //Вызов метода работы с запросами базы данных, с передачей аргументов: строковой переменной с запросом на выборку данных из таблицы "Продукты",
                            // название кэш таблицы, иструкции к алгоритму формирования запроса на изменение данных, не типизированный список с входными данными в запрос
                            dataSetClass.DataSetFill(qrCarrier, "Carrier", DataSetClass.Function.update, values);
                            tbName1Organization.Text = string.Empty;
                            tbTypeOrganization.Text = string.Empty;
                            MessageBox.Show("HOROSHO");
                        }
                        else
                        {
                            MessageBox.Show("Выберите тип, который хотите изменить!");
                            dgCarrier.Focus();
                            LogWriter("Не выбран тип");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Выберите название, который хотите изменить!");
                        dgCarrier.Focus();
                        LogWriter("Не выбрано название");
                    }
                }
                else
                {
                    MessageBox.Show("Выберите значение, который хотите изменить!");
                    dgCarrier.Focus();
                    LogWriter("Не указано значение");
                }
            }
            catch
            {
                MessageBox.Show("Выберите перевозчика, которого хотите изменить!");
                dgCarrier.Focus();
                LogWriter("Не указан перевозчик");
            }
        }

        private void btEmployeeUpdate7_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                values.Clear();
                //Создание экземпляра класса, кэш представление строки данных, с присвоением, с явным преобразованием, выбранной строки таблицы
                DataRowView rowView = dgPost.SelectedItems[0] as DataRowView;
                if (rowView[0] != null)
                {
                    values.Add(rowView[0]);
                    if (!string.IsNullOrEmpty(tbPost.Text))
                    {
                        values.Add(tbPost.Text);
                        DataSetClass dataSetClass = new DataSetClass();
                        //Вызов метода работы с запросами базы данных, с передачей аргументов: строковой переменной с запросом на выборку данных из таблицы "Продукты",
                        // название кэш таблицы, иструкции к алгоритму формирования запроса на изменение данных, не типизированный список с входными данными в запрос
                        dataSetClass.DataSetFill(qrPost, "Post", DataSetClass.Function.update, values);
                        
                        tbPost.Text = string.Empty;
                        MessageBox.Show("HOROSHO");
                    }
                    else
                    {
                        MessageBox.Show("Выберите тип организации, которое хотите изменить!");
                        dgPost.Focus();
                        LogWriter("Не выбран тип организации");
                    }
                }
                else
                {
                    MessageBox.Show("Выберите значение, которое хотите изменить!");
                    dgPost.Focus();
                    LogWriter("Не выбрано значение");
                }
            }
            catch
            {
                MessageBox.Show("Выберите должность, которую хотите изменить!");
                dgPost.Focus();
                LogWriter("Не выбрана должность");
            }
        }

        private void btEmployeeInsert13_Click(object sender, RoutedEventArgs e)
        {
            values.Clear();
            if (!string.IsNullOrEmpty(tbdelivery_Points.Text))
            {
                values.Add(tbdelivery_Points.Text);
                DataSetClass dataSetClass = new DataSetClass();
                dataSetClass.DataSetFill(qrDelivery_Points, "Delivery_Points", DataSetClass.Function.insert, values);
                tbdelivery_Points.Text = string.Empty;
                MessageBox.Show("HOROSHO");
            }

            else
            {
                MessageBox.Show("Введите адрес");
                tbdelivery_Points.Focus();
                LogWriter("Не введен адрес");
            }
        }

        private void btEmployeeUpdate13_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                values.Clear();
                //Создание экземпляра класса, кэш представление строки данных, с присвоением, с явным преобразованием, выбранной строки таблицы
                DataRowView rowView = dgDelivery_Points.SelectedItems[0] as DataRowView;
                if (rowView[0] != null)
                {
                    values.Add(rowView[0]);
                    if (!string.IsNullOrEmpty(tbdelivery_Points.Text))
                    {
                        values.Add(tbdelivery_Points.Text);
                        DataSetClass dataSetClass = new DataSetClass();
                        //Вызов метода работы с запросами базы данных, с передачей аргументов: строковой переменной с запросом на выборку данных из таблицы "Продукты",
                        // название кэш таблицы, иструкции к алгоритму формирования запроса на изменение данных, не типизированный список с входными данными в запрос
                        dataSetClass.DataSetFill(qrDelivery_Points, "Delivery_Points", DataSetClass.Function.update, values);

                        tbdelivery_Points.Text = string.Empty;
                    }
                    else
                    {
                        MessageBox.Show("Выберите адрес, который хотите изменить!");
                        dgDelivery_Points.Focus();
                        LogWriter("Не выбран адрес");
                    }
                }
                else
                {
                    MessageBox.Show("Выберите данные, которую хотите изменить!");
                    dgDelivery_Points.Focus();
                    LogWriter("Не выбраны данные");
                }
            }
            catch
            {
                MessageBox.Show("Выберите адрес, которую хотите изменить!");
                dgDelivery_Points.Focus();
                LogWriter("Не выбран адрес");
            }
        }

        private void btEmployeeUpdate14_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                values.Clear();
                //Создание экземпляра класса, кэш представление строки данных, с присвоением, с явным преобразованием, выбранной строки таблицы
                DataRowView rowView = dgPoints_Route_Sheet.SelectedItems[0] as DataRowView;
                
                if (rowView[0] != null)
                {
                    values.Add(rowView[0]);
                    if (cbPoints_Route_Sheet.SelectedValue != null)
                    {
                        values.Add(cbPoints_Route_Sheet.SelectedValue);
                        if (cbdelivery_Points1.SelectedValue != null)
                        {
                            values.Add(cbdelivery_Points1.SelectedValue);
                            DataSetClass dataSetClass = new DataSetClass();
                            //Вызов метода работы с запросами базы данных, с передачей аргументов: строковой переменной с запросом на выборку данных из таблицы "Продукты",
                            // название кэш таблицы, иструкции к алгоритму формирования запроса на изменение данных, не типизированный список с входными данными в запрос
                            dataSetClass.DataSetFill(qrPoints_Route_Sheet, "Points_Route_Sheet", DataSetClass.Function.update, values);
                            MessageBox.Show("Данные были добавлены!");
                        }
                        else
                        {
                            MessageBox.Show("Выберите номер маршрутного листа, которого хотите изменить!");
                            dgPoints_Route_Sheet.Focus();
                            LogWriter("Не выбран номер маршрутного листа");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Выберите адрес, которое хотите изменить!");
                        dgPoints_Route_Sheet.Focus();
                        LogWriter("Не выбран адрес");
                    }
                }
                else
                {
                    MessageBox.Show("Выберите значение, которое хотите изменить!");
                    dgPoints_Route_Sheet.Focus();
                    LogWriter("Не указано значение");
                }
            }
            catch
            {
                MessageBox.Show("Выберите адрес, которую хотите изменить!");
                dgPoints_Route_Sheet.Focus();
                LogWriter("Не указан адрес");
            }
        }

        private void btEmployeeInsert14_Click(object sender, RoutedEventArgs e)
        {
            values.Clear();
            if (cbPoints_Route_Sheet.SelectedValue != null)
            {
                values.Add(cbPoints_Route_Sheet.SelectedValue);
                if (cbdelivery_Points1.SelectedValue != null)
                {
                    values.Add(cbdelivery_Points1.SelectedValue);
                    DataSetClass dataSetClass = new DataSetClass();
                    //Вызов метода работы с запросами базы данных, с передачей аргументов: строковой переменной с запросом на выборку данных из таблицы "Продукты",
                    // название кэш таблицы, иструкции к алгоритму формирования запроса на изменение данных, не типизированный список с входными данными в запрос
                    dataSetClass.DataSetFill(qrPoints_Route_Sheet, "Points_Route_Sheet", DataSetClass.Function.update, values);
                    MessageBox.Show("Данные были добавлены!");
                }
                else
                {
                    MessageBox.Show("Выберите пункт достаки");
                    cbdelivery_Points1.Focus();
                    LogWriter("Не выбран пункт доставки");
                }
            }
            else
            {
                MessageBox.Show("Выберите маршрут");
                cbPoints_Route_Sheet.Focus();
                LogWriter("Не выбран маршрут");
            }
        }

        private void btProductDelete_Click(object sender, RoutedEventArgs e)
        {
            switch (MessageBox.Show("Удалить выбранную запись?", "Продажа товара", MessageBoxButton.YesNo, MessageBoxImage.Warning))
            {
                case MessageBoxResult.Yes:
                    DataRowView rowView = (DataRowView)dgCargo.SelectedItems[0];
                    values.Clear();
                    if (rowView[0] != null)
                    {
                        values.Add(rowView[0]);
                        //Создание экземпляра класса работы с базой данных
                        DataSetClass dataSetClass = new DataSetClass();
                        //Вызов метода работы с запросами базы данных, с передачей аргументов: строковой переменной с запросом на выборку данных из таблицы "Продукты",
                        // название кэш таблицы, иструкции к алгоритму формирования запроса на удаление данных, не типизированный список с входными данными в запрос
                        dataSetClass.DataSetFill(qrCargo, "Cargo", DataSetClass.Function.delete, values);
                        tbLenght2.Text = string.Empty;
                        tbWidht2.Text = string.Empty;
                        tbHeight2.Text = string.Empty;
                        tbCarrying.Text = string.Empty;
                        tbCapacity.Text = string.Empty;
                        tbNumber.Text = string.Empty;
                    }
                    else
                    {
                        //Вывод сообщения об ошибке, что запись не выбрана в элемента управления
                        MessageBox.Show("Выберите продукт, который хотите удалить!", "Продажа товара");
                        //Перевод курсора и фокуса на указанный визуальный элемент управления
                        dgCargo.Focus();
                        LogWriter("Не выбран продукт");
                    }
                    break;
            }
        }

        private void btBrandDelete_Click(object sender, RoutedEventArgs e)
        {
            switch (MessageBox.Show("Удалить выбранную запись?", "Продажа товара", MessageBoxButton.YesNo, MessageBoxImage.Warning))
            {
                case MessageBoxResult.Yes:
                    DataRowView rowView = (DataRowView)dgMark.SelectedItems[0];
                    values.Clear();
                    if (rowView[0] != null)
                    {
                        values.Add(rowView[0]);
                        //Создание экземпляра класса работы с базой данных
                        DataSetClass dataSetClass = new DataSetClass();
                        //Вызов метода работы с запросами базы данных, с передачей аргументов: строковой переменной с запросом на выборку данных из таблицы "Продукты",
                        // название кэш таблицы, иструкции к алгоритму формирования запроса на удаление данных, не типизированный список с входными данными в запрос
                        dataSetClass.DataSetFill(qrBrand, "Brand", DataSetClass.Function.delete, values);
                        tbBrandName.Text = string.Empty;
                    }
                    else
                    {
                        //Вывод сообщения об ошибке, что запись не выбрана в элемента управления
                        MessageBox.Show("Выберите продукт, который хотите удалить!", "Продажа товара");
                        //Перевод курсора и фокуса на указанный визуальный элемент управления
                        dgMark.Focus();
                        LogWriter("Не выбран продукт");

                    }
                    break;
            }
        }

        private void btEmployeeInsert_Click(object sender, RoutedEventArgs e)
        {
            values.Clear();
            if (!string.IsNullOrEmpty(tbFirstName.Text))
            {
                values.Add(tbFirstName.Text);
                if (!string.IsNullOrEmpty(tbSecondName.Text))
                {
                    values.Add(tbSecondName.Text);
                    if (!string.IsNullOrEmpty(tbMiddleName.Text))
                    {
                        values.Add(tbMiddleName.Text);
                        if (!string.IsNullOrEmpty(tbLogin.Text))
                        {
                            values.Add(tbLogin.Text);
                            if (pbPassword.Password == pbPasswordConf.Password)
                            {
                                values.Add(pbPassword.Password);
                                if (!string.IsNullOrEmpty(tbSNILS.Text))
                                {
                                    values.Add(tbSNILS.Text);
                                    if (!string.IsNullOrEmpty(tbFOMS.Text))
                                    {
                                        values.Add(tbFOMS.Text);
                                        if (cbNameOrganization.SelectedValue != null)
                                        {
                                            values.Add(cbNameOrganization.SelectedValue);
                                            DataSetClass dataSetClass = new DataSetClass();
                                            dataSetClass.DataSetFill(qrEmployee, "Employee", DataSetClass.Function.insert, values);
                                            tbFirstName.Text = string.Empty;
                                            tbSecondName.Text = string.Empty;
                                            tbMiddleName.Text = string.Empty;
                                            tbLogin.Text = string.Empty;
                                            pbPassword.Password = string.Empty;
                                            pbPasswordConf.Password = string.Empty;
                                            tbSNILS.Text = string.Empty;
                                            tbFOMS.Text = string.Empty;
                                            MessageBox.Show("HOROSHO");
                                        }
                                        else
                                        {
                                            MessageBox.Show("Введите название организации");
                                            cbNameOrganization.Focus();
                                            LogWriter("Не введино название организации");

                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Введите ФОМС");
                                        tbFOMS.Focus();
                                        LogWriter("Не ввиден ФОМС");

                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Введите СНИЛС");
                                    tbSNILS.Focus();
                                    LogWriter("Не ввиден СНИЛС");

                                }
                            }
                            else
                            {
                                MessageBox.Show("Пароли не совпадают!", "Продажа товара");
                                //Перевод курсора и фокуса на указанный визуальный элемент управления
                                pbPassword.Focus();
                                LogWriter("Пароли не совпадают");

                            }
                        }
                        else
                        {
                            MessageBox.Show("Введите логин");
                            tbLogin.Focus();
                            LogWriter("Не ввиден логин");

                        }
                    }
                    else
                    {
                        MessageBox.Show("Введите Отчество");
                        tbMiddleName.Focus();
                        LogWriter("Не ввидено отчество");

                    }
                }
                else
                {
                    MessageBox.Show("Введите Имя");
                    tbSecondName.Focus();
                    LogWriter("Не ввидено имя");

                }
            }
            else
            {
                MessageBox.Show("Фамилию");
                tbFirstName.Focus();
                LogWriter("Не ввидена фамилия");
            }
        }

        private void btEmployeeInsert8_Click(object sender, RoutedEventArgs e)
        {
            values.Clear();
            if (!string.IsNullOrEmpty(tbNumber1.Text))
            {
                values.Add(tbNumber1.Text);
                if (!string.IsNullOrEmpty(tbData1.Text))
                {
                    values.Add(tbData1.Text);
                    if (!string.IsNullOrEmpty(tbTime1.Text))
                    {
                        values.Add(tbTime1.Text);
                        if (сbCarrier1.SelectedValue != null)
                        {
                            values.Add(сbCarrier1.SelectedValue);
                            if (сbNone1.SelectedValue != null)
                            {
                                values.Add(сbNone1.SelectedValue);
                                DataSetClass dataSetClass = new DataSetClass();
                                dataSetClass.DataSetFill(qrRoute_Sheet, "Route_Sheet", DataSetClass.Function.insert, values);
                                tbNumber1.Text = string.Empty;
                                tbData1.Text = string.Empty;
                                tbTime1.Text = string.Empty;
                                MessageBox.Show("HOROSHO");
                            }
                            else
                            {
                                MessageBox.Show("Номера");
                                сbCarrier1.Focus();
                                LogWriter("Не введины номера");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Перевозчик");
                            сbCarrier1.Focus();
                            LogWriter("Не введён перевозчик");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Введите время");
                        tbData1.Focus();
                        LogWriter("Не введён маршрут");
                    }
                }
                else
                {
                    MessageBox.Show("Введите дату");
                    tbData1.Focus();
                    LogWriter("Не введена дата");
                }
            }
            else
            {
                MessageBox.Show("Номер маршрутного листа");
                tbNumber1.Focus();
                LogWriter("Не введен маршрут листа");
            }
        }

        private void PostFill()
        {
            try
            {
                DataSetClass dataSetClass = new DataSetClass();
                dataSetClass.DataSetFill(qrPost, "Post", DataSetClass.Function.select, null);
                dgPost.ItemsSource = DataSetClass.dataSet.Tables["Post"].DefaultView;
                dgPost.Columns[0].Visibility = Visibility.Hidden;
                dgPost.Columns[1].Header = "Тип Организации";
            }
            catch (Exception) { }
        }
        private void CarrierFill()
        {
            try
            {
                DataSetClass dataSetClass = new DataSetClass();
                dataSetClass.DataSetFill(qrCarrier, "Carrier", DataSetClass.Function.select, null);
                dgCarrier.ItemsSource = DataSetClass.dataSet.Tables["Carrier"].DefaultView;
                dgCarrier.Columns[0].Visibility = Visibility.Hidden;
                dgCarrier.Columns[1].Header = "Название Организации";
                dgCarrier.Columns[2].Header = "Тип Организации";
            }
            catch (Exception) { }
        }



        private void dgApplication_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btProductInsert_Click(object sender, RoutedEventArgs e)
        {
            values.Clear();
            if (!string.IsNullOrEmpty(tbLenght2.Text))
            {
                values.Add(tbLenght2.Text);

                if (!string.IsNullOrEmpty(tbWidht2.Text))
                {
                    values.Add(tbWidht2.Text);

                    if (!string.IsNullOrEmpty(tbHeight2.Text))
                    {
                        values.Add(tbHeight2.Text);

                        if (!string.IsNullOrEmpty(tbCarrying.Text))
                        {
                            values.Add(tbCarrying.Text);

                            if (!string.IsNullOrEmpty(tbCapacity.Text))
                            {
                                values.Add(tbCapacity.Text);

                                if (!string.IsNullOrEmpty(tbNumber.Text))
                                {
                                    values.Add(tbNumber.Text);

                                    if (cbModel.SelectedValue != null)
                                    {
                                        values.Add(cbModel.SelectedValue);

                                        DataSetClass dataSetClass = new DataSetClass();

                                        dataSetClass.DataSetFill(qrCargo, "Transport", DataSetClass.Function.insert, values);

                                        tbLenght2.Text = string.Empty;
                                        tbWidht2.Text = string.Empty;
                                        tbHeight2.Text = string.Empty;
                                        tbCarrying.Text = string.Empty;
                                        tbCapacity.Text = string.Empty;
                                        tbNumber.Text = string.Empty;
                                    }
                                    else
                                    {
                                        MessageBox.Show("Выберите модель");
                                        LogWriter("Не выбрано значение в списке моделей");
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Введите Номер");
                                    LogWriter("Не вписан номер");
                                }
                            }
                            else
                            {
                                MessageBox.Show("Вместимость");
                                LogWriter("Не вписана вместимость");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Введите грузоподъемность");
                            LogWriter("Не вписан грузоподъемность");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Введите высоту");
                        LogWriter("Не вписана высота");
                    }
                }
                else
                {
                    MessageBox.Show("Введите ширину");
                    LogWriter("Не вписана ширина");
                }
            }
            else
            {
                MessageBox.Show("Введи длина");
                LogWriter("Не вписана длина");

            }
        }

        

        private void CargoFill()
        {
            try
            {
                DataSetClass dataSetClass = new DataSetClass();
                dataSetClass.DataSetFill(qrCargo, "Cargo", DataSetClass.Function.select, null);
                dgCargo.ItemsSource = DataSetClass.dataSet.Tables["Cargo"].DefaultView;
                dgCargo.Columns[0].Visibility = Visibility.Hidden;
                dgCargo.Columns[1].Header = "Длина";
                dgCargo.Columns[2].Header = "Ширина";
                dgCargo.Columns[3].Header = "Высота";
                dgCargo.Columns[4].Header = "Грузоподъемность";
                dgCargo.Columns[5].Header = "Вместимость";
                dgCargo.Columns[6].Header = "Номер";
                dgCargo.Columns[7].Header = "Модель";
            } catch (Exception){ }
        }

        private void dgCountry_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void dgBrand_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void dgEmployee_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void EmployeeFill()
        {
            try
            {
                DataSetClass dataSetClass = new DataSetClass();
                dataSetClass.DataSetFill(qrEmployee, "Employee", DataSetClass.Function.select, null);
                dgEmployee.ItemsSource = DataSetClass.dataSet.Tables["Employee"].DefaultView;
                dgEmployee.Columns[0].Visibility = Visibility.Hidden;
                dgEmployee.Columns[1].Header = "Фамилия";
                dgEmployee.Columns[2].Header = "Имя";
                dgEmployee.Columns[3].Header = "Отчество";
                dgEmployee.Columns[4].Header = "СНИЛС";
                dgEmployee.Columns[5].Header = "ФОМС";
                dgEmployee.Columns[6].Header = "Логин";
                dgEmployee.Columns[7].Header = "Пароль";
                dgEmployee.Columns[8].Header = "Название Организации Перевозчика";
            }
            catch (Exception) { }
        }

        private void ApplicationFill()
        {
            try
            {
                DataSetClass dataSetClass = new DataSetClass();
                dataSetClass.DataSetFill(qrApplication, "Application", DataSetClass.Function.select, null);
                dgApplication.ItemsSource = DataSetClass.dataSet.Tables["Application"].DefaultView;
                dgApplication.Columns[0].Visibility = Visibility.Hidden;
                dgApplication.Columns[1].Header = "Номер Заявки";
                dgApplication.Columns[2].Header = "Дата";
                dgApplication.Columns[3].Header = "Время";
                dgApplication.Columns[4].Header = "Статус";
                dgApplication.Columns[5].Header = "Фамилия";
                dgApplication.Columns[6].Header = "Имя";
                dgApplication.Columns[7].Header = "Отчество";
                dgApplication.Columns[8].Header = "Номер Маршрутного Листа";
            }
            catch (Exception) { }
        }
        private void TransportFill()
        {
            try
            {
                DataSetClass dataSetClass = new DataSetClass();
                dataSetClass.DataSetFill(qrTransport, "Transport", DataSetClass.Function.select, null);
                dgTransport.ItemsSource = DataSetClass.dataSet.Tables["Transport"].DefaultView;
                dgTransport.Columns[0].Visibility = Visibility.Hidden;
                dgTransport.Columns[1].Header = "Описание";
                dgTransport.Columns[2].Header = "Вес";
                dgTransport.Columns[3].Header = "Высота";
                dgTransport.Columns[4].Header = "Ширина";
                dgTransport.Columns[5].Header = "Вместимость";
            }
            catch (Exception) { }
        }

        

        public void cbModelFill()
        {
            DataSetClass dataSetClass = new DataSetClass();
            dataSetClass.DataSetFill(qrModel, "Model", DataSetClass.Function.select, null);
            cbModel.ItemsSource = DataSetClass.dataSet.Tables["Model"].DefaultView;
            //Присвоение в свойство "Путь к выбранному значению (PK)", название столбца первичного ключа, кэш таблицы "Тип продукта"
            cbModel.SelectedValuePath = DataSetClass.dataSet.Tables["Model"].Columns[0].ColumnName;
            cbModel.DisplayMemberPath = DataSetClass.dataSet.Tables["Model"].Columns[1].ColumnName;
            //Присвоение в свойство "Путь к визуальному члену", название столбца "Название типа продукта", кэш таблицы "Тип продукта"

        }


        private void dgCargo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgCargo.Items.Count != 0)
            {
                //Создание экземпляра класса, кэш представление строки данных, с присвоением, с явным преобразованием, выбранной строки таблицы
                DataRowView selectRow = (DataRowView)dgCargo.SelectedItems[0];
                //Присворение в поле ввода "Название продукта", значение из 2 столбца выбранной кэш строки, таблицы "Продукты"
                tbLenght2.Text = selectRow[1].ToString();

                tbWidht2.Text = selectRow[2].ToString();

                tbHeight2.Text = selectRow[3].ToString();
                //Присворение в поле ввода "Цена продукта", значение из 4 столбца выбранной кэш строки, таблицы "Продукты"
                tbCarrying.Text = selectRow[4].ToString();
                //Присворение в поле ввода "Количество продукта", значение из 3 столбца выбранной кэш строки, таблицы "Продукты"
                tbCapacity.Text = selectRow[5].ToString();

                tbNumber.Text = selectRow[6].ToString();
                //Присворение в выпадающий список "Тип продукта", значение из 5 столбца выбранной кэш строки, таблицы "Продукты"

                cbModel.SelectedValue = selectRow[7].ToString();



            }
        }

    }
}
