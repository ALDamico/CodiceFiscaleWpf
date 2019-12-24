using ALD.LibFiscalCode.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ALD.LibFiscalCode.ViewModels
{
    public class MainViewModel:AbstractNotifyPropertyChanged
    {
        public Person CurrentPerson { get; set; } = new Person();

       
    }
}
