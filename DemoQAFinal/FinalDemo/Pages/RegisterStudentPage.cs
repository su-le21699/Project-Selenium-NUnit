using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using AventStack.ExtentReports.Utils;
using FinalDemo.Core;
using FinalDemo.Object;
using OpenQA.Selenium;
using OpenQA.Selenium.Internal;
using Subject = FinalDemo.Object.Subject;

namespace FinalDemo.Pages
{
    public class RegisterStudentPage : BasePage
    {
        private Element _txtFirstName = new Element(By.Id("firstName"));
        private Element _txtLastName = new Element(By.Id("lastName"));
        private Element _txtEmail = new Element(By.Id("userEmail"));
        private Element _lblGender(string gender)
        {
            return new Element(By.XPath($"//label[text()='{gender}']"));
        }
        private Element _txtMobile = new Element(By.Id("userNumber"));
        private Element _txtDateOfBirth = new Element(By.Id("dateOfBirthInput"));
        private Element _lblDay(string day)
        {
            return new Element(By.XPath($"//div[contains(@class,'datepicker') and .='{day}']"));
        }
        private Element _ddlMonth = new Element(By.CssSelector("select[class='react-datepicker__month-select']"));
        private Element _ddlYear = new Element(By.CssSelector("select[class='react-datepicker__year-select']"));
        private Element _txtSubject = new Element(By.Id("subjectsInput"));
        private Element _optSubject(string subject)
        {
            return new Element(By.XPath($"//div[contains(@class,'option') and contains(text(),'{subject}')]"));
        }
        private Element _chkHobby(string hobby)
        {
            return new Element(By.XPath($"//label[text()='{hobby}']"));
        }
        private Element _btnPicture = new Element(By.Id("uploadPicture"));
        private Element _txtCurrentAddress = new Element(By.Id("currentAddress"));
        private Element _ddlState = new Element(By.CssSelector("div#state"));
        private Element _optState(string state)
        {
            return new Element(By.XPath($"//div[contains(text(),'{state}')]"));
        }
        private Element _ddlCity = new Element(By.CssSelector("div#city"));
        private Element _optCity(string city)
        {
            return new Element(By.XPath($"//div[contains(text(),'{city}')]"));
        }
        private Element _btnSubmit = new Element(By.Id("submit"));
        private Element _rowData(string row)
        {
            return new Element(By.XPath($"//td[text()='{row}']/following-sibling::td"));
        }
        private Element _lblConfirm = new Element(By.XPath("//div[contains(text(),'Thanks for submitting the form')]"));
        private Element _popupRegister = new(By.ClassName("modal-content"));


        // public RegisterStudentPage() { }

        public void RegisterStudentAllFields(Student student)
        {
            EnterFirstName(student.FirstName);
            EnterLastName(student.LastName);
            EnterEmail(student.Email);
            SelectGender(student.Gender);
            EnterMobile(student.Mobile);
            EnterDateOfBirth(student.DateOfBirth);
            EnterSubject(student.Subjects);
            SelectHobby(student.Hobbies);
            UploadPicture(student.Picture);
            EnterAddress(student.CurrentAddress);
            EnterState(student.State);
            EnterCity(student.City);
            ClickOnSubmitButton();
        }
        public void RegisterStudentMandatoryFields(Student student)
        {
            EnterFirstName(student.FirstName);
            EnterLastName(student.LastName);
            SelectGender(student.Gender);
            EnterMobile(student.Mobile);
            ClickOnSubmitButton();

        }
        public void VerifyRegister(Student expectedStudent)
        {
            Assert.That(_popupRegister, Is.Not.Null);
            VerifyPopupData(expectedStudent);
        }
        public void VerifyPopupData(Student expectedStudent)
        {
            Assert.That($"{expectedStudent.FirstName} {expectedStudent.LastName}", Is.EqualTo(_rowData("Student Name").GetTextFromElement()));
            VerifyEmail(expectedStudent.Email);
            Assert.That(expectedStudent.Gender, Is.EqualTo(_rowData("Gender").GetTextFromElement()));
            Assert.That(expectedStudent.Mobile, Is.EqualTo(_rowData("Mobile").GetTextFromElement()));
            VerifyDateOfBirth(expectedStudent.DateOfBirth);
            VerifySubjects(expectedStudent.Subjects);
            VerifyHobbies(expectedStudent.Hobbies);
            VerifyPicture(expectedStudent.Picture);
            VerifyAddress(expectedStudent.CurrentAddress);
            VerifyStateCity(expectedStudent.State, expectedStudent.City);
        }
        public void EnterFirstName(string firstName)
        {
            _txtFirstName.ClearText();
            _txtFirstName.EnterText(firstName);
        }
        public void EnterLastName(string lastName)
        {
            _txtLastName.ClearText();
            _txtLastName.EnterText(lastName);
        }
        public void EnterEmail(string email)
        {
            _txtEmail.ClearText();
            _txtEmail.EnterText(email);
        }
        public void EnterMobile(string mobile)
        {
            _txtMobile.ClearText();
            _txtMobile.EnterText(mobile);
        }
        public void EnterAddress(string address)
        {
            _txtCurrentAddress.ClearText();
            _txtCurrentAddress.EnterText(address);
        }


        public void EnterDateOfBirth(string date)
        {
            if (date.IsNullOrEmpty())
                return;
            string[] dateParts = date.Split('-', ' ');
            string day = dateParts[0];
            string month = dateParts[1];
            string year = dateParts[2];

            _txtDateOfBirth.ScrollToElement();
            _txtDateOfBirth.ClickOnElement();
            _ddlYear.SelectByText(year);
            _ddlMonth.SelectByText(month);
            _lblDay(day).ClickOnElement();
        }
        public void SelectGender(string gender)
        {
            if (gender.IsNullOrEmpty())
                return;
            _lblGender(gender).ClickByJS();

        }

        public void EnterSubject(List<Subject> subjects)
        {
            if (subjects.IsNullOrEmpty())
                return;
            foreach (Subject subject in subjects)
            {
                _txtSubject.EnterText(subject.Name);
                _optSubject(subject.Name).ScrollToElement();
                _optSubject(subject.Name).ClickOnElement();
            }
        }
        public void EnterState(string state)
        {
            if (state.IsNullOrEmpty())
                return;
            _ddlState.ClickOnElement();
            _optState(state).ClickOnElement();
        }
        public void EnterCity(string city)
        {
            if (city.IsNullOrEmpty())
                return;
            _ddlCity.ClickOnElement();
            _optCity(city).ClickOnElement();
        }
        public void UploadPicture(string picturePath)
        {
            if (picturePath.IsNullOrEmpty())
                return;
            try
            {
                string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string combinedPath = Path.Combine(baseDirectory, @picturePath);
                string fullPath = Path.GetFullPath(combinedPath);

                _btnPicture.EnterText(fullPath);
            }
            catch (FileNotFoundException)
            {
                throw new FileNotFoundException("Picture file does not exist.", picturePath);
            }
        }
        public void SelectHobby(List<string> hobbies)
        {
            if (hobbies.IsNullOrEmpty())
                return;
            foreach (string hobby in hobbies)
            {
                _chkHobby(hobby).ClickOnElement();
            }
        }
        public void ClickOnSubmitButton()
        {
            _btnSubmit.ScrollToElement();
            _btnSubmit.ClickOnElement();
        }
        public void VerifyStateCity(string state, string city)
        {
            if (state.IsNullOrEmpty() || city.IsNullOrEmpty())
            {
                return;
            }
            Assert.That($"{state} {city}", Is.EqualTo(_rowData("State and City").GetTextFromElement()));
        }
        public void VerifyAddress(string address)
        {
            if (address.IsNullOrEmpty())
            {
                return;
            }
            Assert.That(address, Is.EqualTo(_rowData("Address").GetTextFromElement()));
        }
        public void VerifyPicture(string picture)
        {
            if (picture.IsNullOrEmpty())
            {
                return;
            }
            Assert.That(Path.GetFileName(picture), Is.EqualTo(_rowData("Picture").GetTextFromElement()));
        }
        public void VerifyHobbies(List<string> hobbies)
        {
            if (hobbies.IsNullOrEmpty())
            {
                return;
            }
            var hobbiesText = string.Join(", ", hobbies);
            Assert.That(hobbiesText, Is.EqualTo(_rowData("Hobbies").GetTextFromElement()));
        }
        public void VerifySubjects(List<Subject> subjects)
        {
            if (subjects.IsNullOrEmpty())
            {
                return;
            }
            var actualSubjects = string.Join(", ", subjects.Select(s => s.Name));
            Assert.That(actualSubjects, Is.EqualTo(_rowData("Subjects").GetTextFromElement()));
        }
        public void VerifyDateOfBirth(string dob)
        {
            if (dob.IsNullOrEmpty())
            {
                return;
            }
            Assert.That(ConvertDateFormat(dob), Is.EqualTo(_rowData("Date of Birth").GetTextFromElement()));
        }
        public static string ConvertDateFormat(string inputDate)
        {
            DateTime dateTime = DateTime.ParseExact(inputDate, "d-MMMM-yyyy", CultureInfo.InvariantCulture);
            return dateTime.ToString("dd MMMM,yyyy");
        }
        public void VerifyEmail(string email)
        {
            if (email.IsNullOrEmpty())
            {
                return;
            }
            Assert.That(email, Is.EqualTo(_rowData("Student Email").GetTextFromElement()));
        }

    }
}