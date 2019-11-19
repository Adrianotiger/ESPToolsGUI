using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace esp_tools_gui
{
    class App
    {
        public static MainPage form;
        
        [STAThread]
        static void Main() {
            
            form = new MainPage();
            form.ShowDialog();
        }
    }
}
