using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeChip_App_1._0
{
    internal class ClsPrintTemplate
    {
        string m_printTemplate;
        public ClsPrintTemplate(string Mitarbeiter, string Monat)
        {
            m_printTemplate = "<html><style>table, th, td {  border:1px solid black;border-collapse: collapse;}td{  text-align:center;}table{width:100%; margin:0;}</style>  <body><h1>" + Mitarbeiter + "</h1><h2>" + Monat + "</h2><table><tr><th>Tag</th><th>Buchungen</th><th>Soll</th><th>Ist</th><th>Status</th><th>Ueberstunden</th><th>Monat</th></tr>";
        }

        public void AddLine(string Tag, List<ClsBuchung> buchungen, string Soll, string Ist, string Status, string Überstunden, string Monat)
        {
            m_printTemplate += "<tr><td>" + Tag + "</td><td>" + StringofBuchungen(buchungen) + "</td><td>" + Soll + "</td><td>" + Ist + "</td><td>" + Status + "</td><td>" + Überstunden + "</td><td>" + Monat + "</td></tr>";
        }

        public string GetDoc(string GesSoll, string GesIst, string GesMonatÜberstunden, string GesÜberstunden, string Urlaub)
        {
            m_printTemplate += "<tr /><tr><td /><td /><th>Soll</th><th>Ist</th><th>Ueberstunden</th><th>Gesamt</th><th>Urlaub</th></tr>";
            m_printTemplate += "<tr><th>Gesamt</th><td /><td>" + GesSoll + "</td><td>" + GesIst + "</td><td>" + GesMonatÜberstunden + "</td><td>" + GesÜberstunden + "</td><td>" + Urlaub + "</td></tr>";
            
            m_printTemplate += "</table></body></html>";

            return m_printTemplate;
        }

        private string StringofBuchungen(List<ClsBuchung> buchungen)
        {
            string buchungenfertig = "";
            foreach(ClsBuchung buchung in buchungen)
            {
                buchungenfertig += buchung.ToString();
                buchungenfertig += "<br />";
            }
            return buchungenfertig;
        }
    }
}
