using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Malware2
{
    public class MsgBox
    {
        public static void Mensagem()
        {
            var msg1 = MessageBox.Show("ATENÇÃO!!\n\nDeseja Mesmo Executar Esse Software?\nEle é Potencialmente Perigoso e Destrutivo.\n\n PODE TE TRAZER PREJUIZOS IRREVERSIVÉIS AO COMPUTADOR E QUE EU ( CYBERWARE ) NÃO ME RESPONSABILISO POR NADA!!",
                "TEM CERTEZA??",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Information);

            if (msg1 == DialogResult.No)
            {
                CloseApplication();
            }
            else
            {
                var msg2 = MessageBox.Show("Esse é o Último Aviso, deseja mesmo executar esse Malware?",
                    "ÚLTIMO AVISO!!",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (msg2 == DialogResult.No)
                {
                    CloseApplication();
                }
            }
        }

        public static void CloseApplication()
        {
            string exeName = Path.GetFileNameWithoutExtension(Application.ExecutablePath);
            var processes = Process.GetProcessesByName(exeName);
            foreach (var process in processes)
            {
                process.Kill();
            }
        }
    }
}
