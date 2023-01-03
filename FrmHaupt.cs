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

            UpdateDataView();
            UpdateCldKalender();
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
                DataProvider.InsertMitarbeiter(neu.Mitarbeiternummer,neu.Vorname, neu.Nachname, neu.Arbeitsbeginn, new TimeSpan(0), neu.Arbeitzeitprofil, neu.GetUrlaub());
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
            Bearbeiten.SetUrlaub(zubearbeitender.Urlaub);

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
                zubearbeitender.Urlaub = Bearbeiten.GetUrlaub();

                DataProvider.UpdateMitarbeiter(zubearbeitender);
                m_mitarbeiterliste.ResetBindings();
            }
        }

        private void m_btnLöschen_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Wollen Sie wirklich den Mitarbeiter inklusive aller aufgezeichneten Daten löschen?", "Achtung", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                DataProvider.DeleteMitarbeiter(m_lbxMitarbeiter.SelectedItem as ClsMitarbeiter);
                UpdateMtbtrList();
                UpdateLbxBuchungen();
            }
        }

        private void m_btnarbeitszeitprofil_Click(object sender, EventArgs e)
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
                List<ClsBuchung> Buchungen = DataProvider.SelectAllBuchungenFromDay(m_lbxMitarbeiter.SelectedItem as ClsMitarbeiter, m_cldKalender.SelectionStart, "buchungen");

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

                ClsBerechnung.Berechnen(dlgBuchung.GetDateTime(), ref mitarbeiter, false);

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
                    ClsMitarbeiter mtbtr = m_mitarbeiterliste.ToList().Find(x => x.Mitarbeiternummer.Equals(zubearbeiten.Mitarbeiternummer));

                    DataProvider.UpdateBuchung(zubearbeiten);
                    ClsBerechnung.Berechnen(zubearbeiten.Zeit, ref mtbtr, false);

                    UpdateLbxBuchungen();
                }
            }
        }

        private void m_btnBuchungLöschen_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Wollen Sie wirklich diese Buchung löschen?", "Achtung", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes){
                ClsBuchung buchung = m_lbxBuchungen.SelectedItem as ClsBuchung;
                DataProvider.DeleteBuchung(buchung, "buchungen");

                ClsMitarbeiter mtbtr = m_mitarbeiterliste.ToList().Find(x => x.Mitarbeiternummer.Equals(buchung.Mitarbeiternummer));
                ClsBerechnung.Berechnen(buchung.Zeit, ref mtbtr, false);
                UpdateLbxBuchungen();
            }
        }

        private void UpdateCldKalender()
        {
            List<DateTime> BoldedDates = new List<DateTime>();

            //Statt Select last ausgewerteter Tag, muss ein Tag des ausgewählten Monats selected werden
            DateTime ausgewählter_tag = m_cldKalender.SelectionStart;
            ClsMitarbeiter mitarbeiter = m_lbxMitarbeiter.SelectedItem as ClsMitarbeiter;

            for (int i = 1; i<=DateTime.DaysInMonth(ausgewählter_tag.Year, ausgewählter_tag.Month); i++)
            {
                DateTime tag = new DateTime(ausgewählter_tag.Year, ausgewählter_tag.Month, i);
                if(tag.CompareTo(DateTime.Now) < 0 && tag.CompareTo(mitarbeiter.Arbeitsbeginn) > 0)
                {
                    ClsAusgewerteter_Tag tag1 = DataProvider.SelectAusgewerteterTag(tag, mitarbeiter.Mitarbeiternummer);

                    if(tag1 != null)
                    {
                        if(tag1.Arbeitszeit.Ticks < ClsBerechnung.GetSollArbeitszeit(tag, mitarbeiter).Ticks)
                        {
                            BoldedDates.Add(tag);
                        }
                    }
                    else
                    {
                        BoldedDates.Add(tag);
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

                string urlaub = mitarbeiter.Urlaub.TotalHours.ToString() + ":00";
                if (mitarbeiter.Urlaub.Hours < 0)
                    urlaub = "-" + urlaub;
                Debug.WriteLine(urlaub);

                m_lblUrlaub.Text = urlaub;
                m_lblÜberstunden.Text = mitarbeiter.Überstunden.ToString();

                m_lblSoll.Text = ClsBerechnung.GetSollArbeitszeit(SelectedDay, mitarbeiter).ToString(@"hh\:mm");

                ClsAusgewerteter_Tag tag = DataProvider.SelectAusgewerteterTag(SelectedDay, mitarbeiter.Mitarbeiternummer);
                
                if(tag != null)
                {
                    Debug.WriteLine(tag.ID);
                    Debug.WriteLine(tag.Status);
                    m_lblIst.Text = tag.Arbeitszeit.ToString(@"hh\:mm");
                    switch (tag.Status)
                    {
                        case 0:
                            m_lblTagesstatus.Text = "Zeitausgleich";
                            break;
                        case 1:
                            m_lblTagesstatus.Text = "Krank";
                            break;
                        case 2:
                            m_lblTagesstatus.Text = "Schule";
                            break;
                        case 3:
                            m_lblTagesstatus.Text = "Urlaub";
                            break;
                    }
                }
                else
                {
                    m_lblIst.Text = "00:00";
                    m_lblTagesstatus.Text = "";
                }
            }
        }

        private void m_btnSchule_Click(object sender, EventArgs e)
        {
            ClsAusgewerteter_Tag tag = DataProvider.SelectAusgewerteterTag(m_cldKalender.SelectionStart, (m_lbxMitarbeiter.SelectedItem as ClsMitarbeiter).Mitarbeiternummer);
            if (tag != null)
            {
                int alterStatus = tag.Status;
                tag.Status = 2;

                ClsBerechnung.TagesStatusÄnderung(tag, alterStatus);
                UpdateDataView();
            }
        }

        private void m_btnUrlaub_Click(object sender, EventArgs e)
        {
            ClsAusgewerteter_Tag tag = DataProvider.SelectAusgewerteterTag(m_cldKalender.SelectionStart, (m_lbxMitarbeiter.SelectedItem as ClsMitarbeiter).Mitarbeiternummer);
            if (tag != null)
            {
                int alterStatus = tag.Status;
                tag.Status = 3;

                ClsBerechnung.TagesStatusÄnderung(tag, alterStatus);
                UpdateDataView();
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
                UpdateDataView();
            }
        }

        private void m_btnKrank_Click(object sender, EventArgs e)
        {
            ClsAusgewerteter_Tag tag = DataProvider.SelectAusgewerteterTag(m_cldKalender.SelectionStart, (m_lbxMitarbeiter.SelectedItem as ClsMitarbeiter).Mitarbeiternummer);
            if (tag != null)
            {
                int alterStatus = tag.Status;
                tag.Status = 1;

                ClsBerechnung.TagesStatusÄnderung(tag, alterStatus);
                UpdateDataView();
            }
        }
    }
}
