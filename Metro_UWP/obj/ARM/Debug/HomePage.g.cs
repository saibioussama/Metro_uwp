﻿#pragma checksum "C:\Users\saibi\source\repos\Metro_UWP\Metro_UWP\HomePage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "5180A53DFF648341657902515C202893"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Metro_UWP
{
    partial class HomePage : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                {
                    global::Windows.UI.Xaml.Controls.Page element1 = (global::Windows.UI.Xaml.Controls.Page)(target);
                    #line 11 "..\..\..\HomePage.xaml"
                    ((global::Windows.UI.Xaml.Controls.Page)element1).Loaded += this.Page_Loaded;
                    #line default
                }
                break;
            case 2:
                {
                    this.MyPivot = (global::Windows.UI.Xaml.Controls.Pivot)(target);
                    #line 28 "..\..\..\HomePage.xaml"
                    ((global::Windows.UI.Xaml.Controls.Pivot)this.MyPivot).SelectionChanged += this.MyPivot_SelectionChanged;
                    #line default
                }
                break;
            case 3:
                {
                    this.TimesGridView = (global::Windows.UI.Xaml.Controls.GridView)(target);
                }
                break;
            case 4:
                {
                    this.RemainingTimeTB = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 5:
                {
                    this.StationName = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 6:
                {
                    this.MyListView_ms = (global::Windows.UI.Xaml.Controls.GridView)(target);
                    #line 69 "..\..\..\HomePage.xaml"
                    ((global::Windows.UI.Xaml.Controls.GridView)this.MyListView_ms).ItemClick += this.MyListView_ItemClick;
                    #line default
                }
                break;
            case 7:
                {
                    this.MyListView_sm = (global::Windows.UI.Xaml.Controls.GridView)(target);
                    #line 36 "..\..\..\HomePage.xaml"
                    ((global::Windows.UI.Xaml.Controls.GridView)this.MyListView_sm).ItemClick += this.MyListView_ItemClick;
                    #line default
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}

