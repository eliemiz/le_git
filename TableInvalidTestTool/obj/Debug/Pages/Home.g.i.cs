﻿#pragma checksum "..\..\..\Pages\Home.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "6E3168AFEBF3356524BB7C8C92C4C110"
//------------------------------------------------------------------------------
// <auto-generated>
//     이 코드는 도구를 사용하여 생성되었습니다.
//     런타임 버전:4.0.30319.42000
//
//     파일 내용을 변경하면 잘못된 동작이 발생할 수 있으며, 코드를 다시 생성하면
//     이러한 변경 내용이 손실됩니다.
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


namespace TableInvalidTestTool {
    
    
    /// <summary>
    /// Home
    /// </summary>
    public partial class Home : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 12 "..\..\..\Pages\Home.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView list_view_excel;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\..\Pages\Home.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox check_all;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\..\Pages\Home.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView list_view_tag;
        
        #line default
        #line hidden
        
        
        #line 57 "..\..\..\Pages\Home.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox check_all_tag;
        
        #line default
        #line hidden
        
        
        #line 66 "..\..\..\Pages\Home.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox text_box_tag;
        
        #line default
        #line hidden
        
        
        #line 67 "..\..\..\Pages\Home.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button add_tag;
        
        #line default
        #line hidden
        
        
        #line 68 "..\..\..\Pages\Home.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button remove_tag;
        
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
            System.Uri resourceLocater = new System.Uri("/TableInvalidTestTool;component/pages/home.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Pages\Home.xaml"
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
            this.list_view_excel = ((System.Windows.Controls.ListView)(target));
            return;
            case 2:
            this.check_all = ((System.Windows.Controls.CheckBox)(target));
            
            #line 24 "..\..\..\Pages\Home.xaml"
            this.check_all.Click += new System.Windows.RoutedEventHandler(this.OnClickCheckAllTable);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 37 "..\..\..\Pages\Home.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.OnClickAddTable);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 38 "..\..\..\Pages\Home.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.OnClickRemoveTable);
            
            #line default
            #line hidden
            return;
            case 5:
            this.list_view_tag = ((System.Windows.Controls.ListView)(target));
            return;
            case 6:
            this.check_all_tag = ((System.Windows.Controls.CheckBox)(target));
            
            #line 57 "..\..\..\Pages\Home.xaml"
            this.check_all_tag.Click += new System.Windows.RoutedEventHandler(this.OnClickCheckAllTag);
            
            #line default
            #line hidden
            return;
            case 7:
            this.text_box_tag = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            this.add_tag = ((System.Windows.Controls.Button)(target));
            
            #line 67 "..\..\..\Pages\Home.xaml"
            this.add_tag.Click += new System.Windows.RoutedEventHandler(this.OnClickAddTag);
            
            #line default
            #line hidden
            return;
            case 9:
            this.remove_tag = ((System.Windows.Controls.Button)(target));
            
            #line 68 "..\..\..\Pages\Home.xaml"
            this.remove_tag.Click += new System.Windows.RoutedEventHandler(this.OnClickRemoveTag);
            
            #line default
            #line hidden
            return;
            case 10:
            
            #line 73 "..\..\..\Pages\Home.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.OnClickStart);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

