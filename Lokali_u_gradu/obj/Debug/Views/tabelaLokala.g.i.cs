﻿#pragma checksum "..\..\..\Views\tabelaLokala.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "80BCA95BE3B7F3843289F8A603985856"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Lokali_u_gradu.Views;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
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


namespace Lokali_u_gradu.Views {
    
    
    /// <summary>
    /// tabelaLokala
    /// </summary>
    public partial class tabelaLokala : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 26 "..\..\..\Views\tabelaLokala.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtPretraga;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\Views\tabelaLokala.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image lupaIMG;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\Views\tabelaLokala.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid tableGridLokali;
        
        #line default
        #line hidden
        
        
        #line 57 "..\..\..\Views\tabelaLokala.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnDodajLokal;
        
        #line default
        #line hidden
        
        
        #line 58 "..\..\..\Views\tabelaLokala.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnIzmeniLokal;
        
        #line default
        #line hidden
        
        
        #line 59 "..\..\..\Views\tabelaLokala.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnObrisiLokal;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Lokali_u_gradu;component/views/tabelalokala.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Views\tabelaLokala.xaml"
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
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.txtPretraga = ((System.Windows.Controls.TextBox)(target));
            
            #line 26 "..\..\..\Views\tabelaLokala.xaml"
            this.txtPretraga.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.txtPretraga_TextChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.lupaIMG = ((System.Windows.Controls.Image)(target));
            return;
            case 3:
            this.tableGridLokali = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 4:
            this.btnDodajLokal = ((System.Windows.Controls.Button)(target));
            
            #line 57 "..\..\..\Views\tabelaLokala.xaml"
            this.btnDodajLokal.Click += new System.Windows.RoutedEventHandler(this.btnDodajLokal_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.btnIzmeniLokal = ((System.Windows.Controls.Button)(target));
            
            #line 58 "..\..\..\Views\tabelaLokala.xaml"
            this.btnIzmeniLokal.Click += new System.Windows.RoutedEventHandler(this.btnIzmeniLokal_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.btnObrisiLokal = ((System.Windows.Controls.Button)(target));
            
            #line 59 "..\..\..\Views\tabelaLokala.xaml"
            this.btnObrisiLokal.Click += new System.Windows.RoutedEventHandler(this.btnObrisiLokal_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

