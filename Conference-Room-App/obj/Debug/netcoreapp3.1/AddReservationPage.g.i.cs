﻿#pragma checksum "..\..\..\AddReservationPage.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "6ED186589BAA6F688FBA4DE8B6923633E6292A65"
//------------------------------------------------------------------------------
// <auto-generated>
//     Ten kod został wygenerowany przez narzędzie.
//     Wersja wykonawcza:4.0.30319.42000
//
//     Zmiany w tym pliku mogą spowodować nieprawidłowe zachowanie i zostaną utracone, jeśli
//     kod zostanie ponownie wygenerowany.
// </auto-generated>
//------------------------------------------------------------------------------

using Conference_Room_App;
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


namespace Conference_Room_App {
    
    
    /// <summary>
    /// AddReservationPage
    /// </summary>
    public partial class AddReservationPage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 19 "..\..\..\AddReservationPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Calendar Calendar1;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\AddReservationPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox FromHour;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\..\AddReservationPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox FromMinute;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\..\AddReservationPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox ToHour;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\..\AddReservationPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox ToMinute;
        
        #line default
        #line hidden
        
        
        #line 64 "..\..\..\AddReservationPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox ComboboxPerson;
        
        #line default
        #line hidden
        
        
        #line 77 "..\..\..\AddReservationPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel StackPanel_Rooms;
        
        #line default
        #line hidden
        
        
        #line 81 "..\..\..\AddReservationPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lbl;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.6.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Conference-Room-App;component/addreservationpage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\AddReservationPage.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.6.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.Calendar1 = ((System.Windows.Controls.Calendar)(target));
            
            #line 20 "..\..\..\AddReservationPage.xaml"
            this.Calendar1.SelectedDatesChanged += new System.EventHandler<System.Windows.Controls.SelectionChangedEventArgs>(this.Calendar1_SelectedDatesChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.FromHour = ((System.Windows.Controls.ComboBox)(target));
            
            #line 31 "..\..\..\AddReservationPage.xaml"
            this.FromHour.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.FromHour_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.FromMinute = ((System.Windows.Controls.ComboBox)(target));
            
            #line 38 "..\..\..\AddReservationPage.xaml"
            this.FromMinute.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.FromMinute_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.ToHour = ((System.Windows.Controls.ComboBox)(target));
            
            #line 48 "..\..\..\AddReservationPage.xaml"
            this.ToHour.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ToHour_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.ToMinute = ((System.Windows.Controls.ComboBox)(target));
            
            #line 55 "..\..\..\AddReservationPage.xaml"
            this.ToMinute.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ToMinute_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 6:
            this.ComboboxPerson = ((System.Windows.Controls.ComboBox)(target));
            
            #line 69 "..\..\..\AddReservationPage.xaml"
            this.ComboboxPerson.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ComboboxPerson_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 7:
            this.StackPanel_Rooms = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 8:
            this.lbl = ((System.Windows.Controls.Label)(target));
            return;
            case 9:
            
            #line 88 "..\..\..\AddReservationPage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Reservation_Button_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

