using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace TimeChip_App_1._0
{
    public partial class FrmHaupt : Form
    {
        static BindingList<ClsMitarbeiter> m_mitarbeiterliste = new BindingList<ClsMitarbeiter>();

        public FrmHaupt()
        {
            InitializeComponent();

            m_lbxMitarbeiter.DataSource = m_mitarbeiterliste;

            UpdateMtbtrList();

            m_lbxMitarbeiter.SelectedIndex = 1;

            UpdateLbxBuchungen();

            ClsBerechnung.Berechnen();

            UpdateCldKalender();
            UpdateDataView();
        }

        public static BindingList<ClsMitarbeiter> Mitarbeiterliste { get { return m_mitarbeiterliste; } set { m_mitarbeiterliste = value; } }

        private void m_btnNeu_Click(object sender, EventArgs e)
        {
            DlgMitarbeiter neu = new DlgMitarbeiter();
            neu.Titel = "Neuer:e Mitarbeiter:in";
            neu.OkKnopf = "Erstellen";
            neu.Name = "Neu";
            neu.BtnAddFinger = "Finger hinzufügen";
            neu.BtnAddCard = "Karte hinzufügen";
            neu.Bearbeiten = false;
            if (neu.ShowDialog() == DialogResult.OK)
            {
                DataProvider.InsertMitarbeiter(neu.Mitarbeiternummer,neu.Vorname, neu.Nachname, neu.Arbeitsbeginn, new TimeSpan(0), neu.Arbeitzeitprofil, new TimeSpan(0));
                UpdateMtbtrList();
                UpdateLbxBuchungen();
            }
        }

        private void m_btnBearbeiten_Click(object sender, EventArgs e)
        {
            DlgMitarbeiter Bearbeiten = new DlgMitarbeiter();

            /*
            for(int i = 0; i<m_arbeitsprofilliste.Count; i++)
            {
                Bearbeiten.m_cmbxAProfil.Items.Add(m_arbeitsprofilliste[i]);
            }
            */

            ClsMitarbeiter zubearbeitender = m_lbxMitarbeiter.SelectedItem as ClsMitarbeiter;
            Bearbeiten.Titel = "Mitarbeiter:in bearbeiten";
            Bearbeiten.OkKnopf = "Bearbeiten";
            Bearbeiten.Name = "Bearbeiten";
            Bearbeiten.BtnAddFinger = "Finger ändern";
            Bearbeiten.BtnAddCard = "Karte ändern";
            Bearbeiten.Vorname = zubearbeitender.Vorname;
            Bearbeiten.Nachname = zubearbeitender.Nachname;
            Bearbeiten.Arbeitsbeginn = zubearbeitender.Arbeitsbeginn;
            Bearbeiten.Bearbeiten = true;
            Bearbeiten.Mitarbeiternummer = zubearbeitender.Mitarbeiternummer;

            List<ClsArbeitsprofil> clsArbeitsprofils = DlgArbeitszeitprofile.ArbeitsprofilListe.ToList();
            ClsArbeitsprofil arbeitsprofil = clsArbeitsprofils.FindLast(x => x.ID.Equals(zubearbeitender.Arbeitszeitprofil.ID));
            Bearbeiten.Arbeitzeitprofil = arbeitsprofil;

            if (Bearbeiten.ShowDialog() == DialogResult.OK)
            {
                zubearbeitender.Vorname = Bearbeiten.Vorname;
                zubearbeitender.Nachname = Bearbeiten.Nachname;
                zubearbeitender.Arbeitsbeginn = Bearbeiten.Arbeitsbeginn;
                zubearbeitender.Arbeitszeitprofil = Bearbeiten.Arbeitzeitprofil;
                zubearbeitender.Mitarbeiternummer = Bearbeiten.Mitarbeiternummer;

                DataProvider.UpdateMitarbeiter(zubearbeitender);
                m_mitarbeiterliste.ResetBindings();
            }
        }

        private void m_btnLöschen_Click(object sender, EventArgs e)
        {
            DataProvider.DeleteMitarbeiter(m_lbxMitarbeiter.SelectedItem as ClsMitarbeiter);
            UpdateMtbtrList();
            UpdateLbxBuchungen();
        }

        private void m_btnarbeitsprofil_Click(object sender, EventArgs e)
        {
            DlgArbeitszeitprofile arbeitszeitprofile = new DlgArbeitszeitprofile();
            arbeitszeitprofile.ShowDialog();
        }

        private void UpdateMtbtrList()
        {
            m_mitarbeiterliste.Clear();

            foreach(ClsMitarbeiter mtbtr in DataProvider.SelectAllMitarbeiter())
            {
                m_mitarbeiterliste.Add(mtbtr);
            }

            m_mitarbeiterliste.ResetBindings();
        }

        private void m_lbxMitarbeiterChanged(object sender, EventArgs e)
        {
            UpdateLbxBuchungen();
        }
        private void m_cldKalenderChanged(object sender, DateRangeEventArgs e)
        {
            UpdateLbxBuchungen();
        }

        private void UpdateLbxBuchungen()
        {
            if (m_lbxMitarbeiter.SelectedItem != null)
            {
                List<ClsBuchung> Buchungen = DataProvider.SelectAllBuchungenFromDay((m_lbxMitarbeiter.SelectedItem as ClsMitarbeiter).Mitarbeiternummer, m_cldKalender.SelectionStart);

                //List<ClsBuchung> Buchungen2 = Buchungen.FindAll(x => x.Zeit.Date.Equals(m_cldKalender.SelectionStart));

                m_lbxBuchungen.Items.Clear();

                m_lbxBuchungen.Items.AddRange(Buchungen.ToArray());

                UpdateCldKalender();
                UpdateDataView();
            }
        }

        private void m_btnNeueBuchung_Click(object sender, EventArgs e)
        {
            DlgBuchung dlgBuchung = new DlgBuchung();

            dlgBuchung.Titel = "Buchung hinzufügen";
            dlgBuchung.OkKnopf = "Hinzufügen";
            dlgBuchung.Name = "Neu";

            List<ClsMitarbeiter> clsMitarbeiters = m_mitarbeiterliste.ToList();
            ClsMitarbeiter mitarbeiter = clsMitarbeiters.FindLast(x => x.ID.Equals((m_lbxMitarbeiter.SelectedItem as ClsMitarbeiter).ID));
            dlgBuchung.Mitarbeiter = mitarbeiter;

            if (dlgBuchung.ShowDialog() == DialogResult.OK) 
            {
                DataProvider.InsertBuchung(dlgBuchung.Buchungstyp, dlgBuchung.GetDateTime(), dlgBuchung.Mitarbeiter.Mitarbeiternummer);

                ClsBerechnung.Berechnen(dlgBuchung.GetDateTime(), m_mitarbeiterliste.ToList().Find(x => x.Mitarbeiternummer.Equals(mitarbeiter.Mitarbeiternummer)));

                UpdateLbxBuchungen();
            }
        }

        private void m_btnBuchungBearbeiten_Click(object sender, EventArgs e)
        {
            if (m_lbxBuchungen.SelectedItem != null)
            {
                DlgBuchung dlgBuchung = new DlgBuchung();

                ClsBuchung zubearbeiten = m_lbxBuchungen.SelectedItem as ClsBuchung;
                dlgBuchung.Titel = "Buchung bearbeiten";
                dlgBuchung.OkKnopf = "Bearbeiten";
                dlgBuchung.Name = "Bearbeiten";
                dlgBuchung.SetBuchungstyp(zubearbeiten.Buchungstyp);
                dlgBuchung.Buchungstyp = zubearbeiten.Buchungstyp;
                dlgBuchung.SetDateTime(zubearbeiten.Zeit);
                dlgBuchung.Mitarbeiter = m_mitarbeiterliste.ToList().FindLast(x => x.Mitarbeiternummer.Equals(zubearbeiten.Mitarbeiternummer));

                if(dlgBuchung.ShowDialog() == DialogResult.OK)
                {
                    zubearbeiten.Buchungstyp = dlgBuchung.Buchungstyp;
                    zubearbeiten.Zeit = dlgBuchung.GetDateTime();
                    zubearbeiten.Mitarbeiternummer = dlgBuchung.Mitarbeiter.Mitarbeiternummer;

                    DataProvider.UpdateBuchung(zubearbeiten);
                    ClsBerechnung.Berechnen(zubearbeiten.Zeit, m_mitarbeiterliste.ToList().Find(x => x.Mitarbeiternummer.Equals(zubearbeiten.Mitarbeiternummer)));

                    UpdateLbxBuchungen();
                }
            }
        }

        private void m_btnBuchungLöschen_Click(object sender, EventArgs e)
        {
            ClsBuchung buchung = m_lbxBuchungen.SelectedItem as ClsBuchung;
            DataProvider.DeleteBuchung(buchung, "buchungen");

            ClsBerechnung.Berechnen(buchung.Zeit, m_mitarbeiterliste.ToList().Find(x => x.Mitarbeiternummer.Equals(buchung.Mitarbeiternummer)));
            UpdateLbxBuchungen();
        }

        private void UpdateCldKalender()
        {
            List<DateTime> BoldedDates = new List<DateTime>();

            ClsAusgewerteter_Tag tag = DataProvider.SelectLastAusgewerteterTag();

            for (int i = 1; i<=DateTime.DaysInMonth(m_cldKalender.SelectionStart.Year, m_cldKalender.SelectionStart.Month); i++)
            {
                if(tag.Date.CompareTo(new DateTime(m_cldKalender.SelectionStart.Year, m_cldKalender.SelectionStart.Month, i)) > 0)
                {
                    ClsAusgewerteter_Tag tag1 = DataProvider.SelectAusgewerteterTag(new DateTime(m_cldKalender.SelectionStart.Year, m_cldKalender.SelectionStart.Month, i), (m_lbxMitarbeiter.SelectedItem as ClsMitarbeiter).Mitarbeiternummer);

                    if (tag1 == null)
                    {
                        switch (new DateTime(m_cldKalender.SelectionStart.Year, m_cldKalender.SelectionStart.Month, i).DayOfWeek)
                        {
                            case DayOfWeek.Monday:
                                if (new TimeSpan(0).Ticks <= (m_lbxMitarbeiter.SelectedItem as ClsMitarbeiter).Arbeitszeitprofil.Montag.Arbeitszeit.Ticks)
                                {
                                    BoldedDates.Add(new DateTime(m_cldKalender.SelectionStart.Year, m_cldKalender.SelectionStart.Month, i));
                                }
                                break;
                            case DayOfWeek.Tuesday:
                                if (new TimeSpan(0).Ticks <= (m_lbxMitarbeiter.SelectedItem as ClsMitarbeiter).Arbeitszeitprofil.Dienstag.Arbeitszeit.Ticks)
                                {
                                    BoldedDates.Add(new DateTime(m_cldKalender.SelectionStart.Year, m_cldKalender.SelectionStart.Month, i));
                                }
                                break;
                            case DayOfWeek.Wednesday:
                                if (new TimeSpan(0).Ticks <= (m_lbxMitarbeiter.SelectedItem as ClsMitarbeiter).Arbeitszeitprofil.Mittwoch.Arbeitszeit.Ticks)
                                {
                                    BoldedDates.Add(new DateTime(m_cldKalender.SelectionStart.Year, m_cldKalender.SelectionStart.Month, i));
                                }
                                break;
                            case DayOfWeek.Thursday:
                                if (new TimeSpan(0).Ticks <= (m_lbxMitarbeiter.SelectedItem as ClsMitarbeiter).Arbeitszeitprofil.Donnerstag.Arbeitszeit.Ticks)
                                {
                                    BoldedDates.Add(new DateTime(m_cldKalender.SelectionStart.Year, m_cldKalender.SelectionStart.Month, i));
                                }
                                break;
                            case DayOfWeek.Friday:
                                if (new TimeSpan(0).Ticks <= (m_lbxMitarbeiter.SelectedItem as ClsMitarbeiter).Arbeitszeitprofil.Freitag.Arbeitszeit.Ticks)
                                {
                                    BoldedDates.Add(new DateTime(m_cldKalender.SelectionStart.Year, m_cldKalender.SelectionStart.Month, i));
                                }
                                break;
                            case DayOfWeek.Saturday:
                                if (new TimeSpan(0).Ticks <= (m_lbxMitarbeiter.SelectedItem as ClsMitarbeiter).Arbeitszeitprofil.Samstag.Arbeitszeit.Ticks)
                                {
                                    BoldedDates.Add(new DateTime(m_cldKalender.SelectionStart.Year, m_cldKalender.SelectionStart.Month, i));
                                }
                                break;
                            case DayOfWeek.Sunday:
                                if (new TimeSpan(0).Ticks <= (m_lbxMitarbeiter.SelectedItem as ClsMitarbeiter).Arbeitszeitprofil.Sonntag.Arbeitszeit.Ticks)
                                {
                                    BoldedDates.Add(new DateTime(m_cldKalender.SelectionStart.Year, m_cldKalender.SelectionStart.Month, i));
                                }
                                break;
                        }
                    }
                }
            }
            m_cldKalender.BoldedDates = BoldedDates.ToArray();

        }

        private void UpdateDataView()
        {
            if(m_lbxMitarbeiter.SelectedItems != null)
            {
                DateTime SelectedDay = m_cldKalender.SelectionStart;
                ClsMitarbeiter mitarbeiter = m_lbxMitarbeiter.SelectedItem as ClsMitarbeiter;

                m_lblUrlaub.Text = mitarbeiter.Urlaub.ToString();

                m_lblSoll.Text = ClsBerechnung.GetSollArbeitszeit(SelectedDay, mitarbeiter).ToString(@"hh\:mm");

                ClsAusgewerteter_Tag tag = DataProvider.SelectAusgewerteterTag(SelectedDay, mitarbeiter.Mitarbeiternummer);

                if(tag != null)
                {
                    m_lblIst.Text = tag.Arbeitszeit.ToString(@"hh\:mm");
                }
                else
                {
                    m_lblIst.Text = "00:00";
                }
            }
        }

        private void m_btnEntschuldigt_Click(object sender, EventArgs e)
        {
            ClsAusgewerteter_Tag tag = DataProvider.SelectAusgewerteterTag(m_cldKalender.SelectionStart, (m_lbxMitarbeiter.SelectedItem as ClsMitarbeiter).Mitarbeiternummer);
            if (tag != null)
            {
                int alterStatus = tag.Status;
                tag.Status = 1;

                ClsBerechnung.TagesStatusÄnderung(tag, alterStatus);
            }
        }

        private void m_btnUrlaub_Click(object sender, EventArgs e)
        {
            ClsAusgewerteter_Tag tag = DataProvider.SelectAusgewerteterTag(m_cldKalender.SelectionStart, (m_lbxMitarbeiter.SelectedItem as ClsMitarbeiter).Mitarbeiternummer);
            if (tag != null)
            {
                int alterStatus = tag.Status;
                tag.Status = 2;

                ClsBerechnung.TagesStatusÄnderung(tag, alterStatus);
            }
        }

        private void m_btnUnentschuldigt_Click(object sender, EventArgs e)
        {
            ClsAusgewerteter_Tag tag = DataProvider.SelectAusgewerteterTag(m_cldKalender.SelectionStart, (m_lbxMitarbeiter.SelectedItem as ClsMitarbeiter).Mitarbeiternummer);
            if (tag != null)
            {
                int alterStatus = tag.Status;
                tag.Status = 0;

                ClsBerechnung.TagesStatusÄnderung(tag, alterStatus);
            }
        }
    }
}
