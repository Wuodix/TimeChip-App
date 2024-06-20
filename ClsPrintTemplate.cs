using System.Collections.Generic;

namespace TimeChip_App
{
    internal class ClsPrintTemplate
    {
        string m_printTemplate;
        public ClsPrintTemplate(string Mitarbeiter, string Monat)
        {
            m_printTemplate = "<html><style>table, th, td {  border:1px solid black;border-collapse: collapse;}td{  text-align: center; font-size: 11px; height:25px }table{table-layout:fixed; width: 100%; margin:0;}</style>  <body><h1>" + Mitarbeiter + "</h1><h2>" + Monat + "</h2><table><tr><th style='width:10%'>Tag</th><th style='width:10%'>Buchungen</th><th style='width:10%'>Soll</th><th style='width:10%'>Ist</th><th style='width:13%'>Status</th><th style='width:15%'>Überstunden</th><th style='width:10%'>Monat</th></tr>";
        }

        /// <summary>
        /// Fügt eine neue Zeile an der auszudruckenden Monatsübersicht hinzu
        /// </summary>
        /// <param name="Tag">Das Datum des Tages, dessen Zeile hinzugefügt werden soll</param>
        /// <param name="buchungen">Eine Liste aus Buchungen, die der betroffene Mitarbeiter an Tag gemacht hat</param>
        /// <param name="Soll">Die Soll Arbeitszeit vom Betroffenen Mitarbeiter am betroffenen Tag</param>
        /// <param name="Ist">Die tatsächlich gearbeitete Zeit vom betroffenen Mitarbeiter am betroffenen Tag</param>
        /// <param name="Status">Der Status des Tages</param>
        /// <param name="Überstunden">Die Überstunden des Mitarbeiters an Tag</param>
        /// <param name="Monat">Die Überstunden, die der betroffene Mitarbeiter bis zum aktuellen Tag in diesem Monat bereits gesammelt hat</param>
        public void AddLine(string Tag, List<ClsBuchung> buchungen, string Soll, string Ist, string Status, string Überstunden, string Monat)
        {
            m_printTemplate += "<tr><td>" + Tag + "</td><td>" + StringofBuchungen(buchungen) + "</td><td>" + Soll + "</td><td>" + Ist + "</td><td>" + Status + "</td><td>" + Überstunden + "</td><td>" + Monat + "</td></tr>";
        }

        /// <summary>
        /// Beendet den Aufbau und die Dateneintragung der auszudruckenden Monatsübersicht und gibt diesen als HTML-Dokument in Form eines Strings zurück
        /// </summary>
        /// <param name="GesSoll">Die gesamte Arbeitszeit, die der betroffene Mitarbeiter im betroffenen Monat zu arbeiten hatte</param>
        /// <param name="GesIst">Die gesamte Zeit, die der betroffene Mitarbeiter im betroffenen Monat tatsächlich gearbeitet hat</param>
        /// <param name="GesMonatÜberstunden">Die gesamten Überstunden, die der betroffene Mitarbeiter im betroffenen Monat gesammelt hat</param>
        /// <param name="GesÜberstunden">Die gesamten Überstunden, die der betroffene Mitarbeiter zum Ende des betroffenen Monats hatte</param>
        /// <param name="Urlaub">Der gesamte restliche Urlaub, den der betroffene Mitarbeiter zum Ende des betroffenen Monats noch zur Verfügung hatte</param>
        /// <returns>Die mit Daten gefüllte Monatsübersicht als HTML-String</returns>
        public string GetDoc(string GesSoll, string GesIst, string GesMonatÜberstunden, string GesÜberstunden, string Urlaub)
        {
            m_printTemplate += "<tr /><tr><td /><td /><th>Soll</th><th>Ist</th><th>Überstunden</th><th>Gesamt</th><th>Urlaub</th></tr>";
            m_printTemplate += "<tr><th>Gesamt</th><td /><td>" + GesSoll + "</td><td>" + GesIst + "</td><td>" + GesMonatÜberstunden + "</td><td>" + GesÜberstunden + "</td><td>" + Urlaub + "</td></tr>";
            
            m_printTemplate += "</table></body></html>";

            return m_printTemplate;
        }

        /// <summary>
        /// Wandelt eine Liste aus Buchungen in einen String um, der im HTML-String für die Monatsübersicht verwendet werden kann
        /// </summary>
        /// <param name="buchungen">Die List aus umzuwandelnden Buchungen</param>
        /// <returns>Die unzuwandelnden Buchungen als HTML-String dargestellt</returns>
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
