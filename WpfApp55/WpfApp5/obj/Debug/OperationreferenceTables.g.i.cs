// Updated by XamlIntelliSenseFileGenerator 08.04.2022 10:24:09
#pragma checksum "..\..\OperationreferenceTables.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "F4042D3C3DE2A39E82F626F3E89F021845D047DAE2F43C0481B902F38A918A0A"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using WpfApp5;


namespace WpfApp5
{


    /// <summary>
    /// OperationreferenceTables
    /// </summary>
    public partial class OperationreferenceTables : System.Windows.Window, System.Windows.Markup.IComponentConnector
    {

#line default
#line hidden

        private bool _contentLoaded;

        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent()
        {
            if (_contentLoaded)
            {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/WpfApp5;component/operationreferencetables.xaml", System.UriKind.Relative);

#line 1 "..\..\OperationreferenceTables.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);

#line default
#line hidden
        }

        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:
                    this.dgCountry = ((System.Windows.Controls.DataGrid)(target));

#line 17 "..\..\OperationreferenceTables.xaml"
                    this.dgCountry.Loaded += new System.Windows.RoutedEventHandler(this.dgCountry_Loaded);

#line default
#line hidden

#line 17 "..\..\OperationreferenceTables.xaml"
                    this.dgCountry.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.dgCountry_SelectionChanged);

#line default
#line hidden
                    return;
                case 2:
                    this.dtCountry = ((System.Windows.Controls.TextBox)(target));

#line 23 "..\..\OperationreferenceTables.xaml"
                    this.dtCountry.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.TextBox_TextChanged);

#line default
#line hidden
                    return;
            }
            this._contentLoaded = true;
        }
    }
}

