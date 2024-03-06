using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASM_C_3
{
    internal class SV
    {
        private string _masv;
        private string _name;
        private double _ta;
        private double _th;
        private double _gdtc;
        private double _diemtb;
        public SV()
        {

        }
        public SV(string masv, string name , double ta, double th, double gdtc, double diemtb)
        {
            _masv = masv;
            _name = name;
            _ta = ta;
            _th = th;
            _gdtc = gdtc;
            _diemtb = diemtb;
        }

        public string Masv { get => _masv; set => _masv = value; }
        public string Name { get => _name; set => _name = value; }
        public double Ta { get => _ta; set => _ta = value; }
        public double Th { get => _th; set => _th = value; }
        public double Gdtc { get => _gdtc; set => _gdtc = value; }
        public double Diemtb { get=>_diemtb; set => _diemtb = value;}
    }
}
