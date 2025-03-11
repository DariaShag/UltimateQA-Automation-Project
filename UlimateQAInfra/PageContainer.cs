using OpenQA.Selenium;
using System.Linq;

namespace UltimateQAInfra
{
    public class PageContainer
    {
        protected static IWebElement _mainWindow;
        protected static IWebElement _mainArea;
        protected static IReadOnlyCollection<IWebElement> _sections;
        public PageContainer(IWebElement mainWindow)
        {
            _mainWindow = mainWindow;
            _mainArea = _mainWindow.FindElement(By.Id("et-main-area"));
            _sections = _mainArea.FindElements(By.ClassName("et_pb_row"));
        }

        public PageContainer()
        {

        }
    }

    public class ButtonsSection:PageContainer
    {
        protected static IReadOnlyCollection<IWebElement> _buttons;
        protected static IWebElement _buttonsSection;
        public IReadOnlyCollection<IWebElement> Buttons
        {
            get => _buttons;
        }

        public ButtonsSection() 
        {
            _buttonsSection = _sections.ElementAt(2);
            _buttons = _buttonsSection.FindElements(By.ClassName("et_pb_button_module_wrapper"));
        }
    }

    public class SocialsSection : PageContainer
    {
        protected IWebElement _socialsSection;
        protected static IReadOnlyCollection<IWebElement> _socials;
        public List<SocialMediaIcon> Socials;
        public SocialsSection()
        {
            _socialsSection = _sections.ElementAt(4);
            _socials = _socialsSection.FindElements(By.ClassName("et_pb_social_network_link"));
            Socials = new List<SocialMediaIcon>();
            foreach (var i in _socials)
            {
                Socials.Add(new SocialMediaIcon(i));
            }
        }

        public class SocialMediaIcon
        {
            public string Name;
            public string Link;

            public SocialMediaIcon(IWebElement i)
            {
                Name = i.FindElement(By.ClassName("et_pb_with_border")).GetAttribute("title");
                Link = i.FindElement(By.ClassName("et_pb_with_border")).GetAttribute("href");
            }
        }
    }

    public class RandomStuffSection : PageContainer
    {

        protected IWebElement _randomStuffSection;
        private IReadOnlyCollection<IWebElement> _contactForms;
        public List<ContactForm> ContactForms
        {
            get => _forms;
        }

        private  static List<ContactForm> _forms;

        public RandomStuffSection()
        {
            Thread.Sleep(7000);
            _randomStuffSection = _sections.ElementAt(6);
            _contactForms = _randomStuffSection.FindElements(By.ClassName("et_pb_contact_form_container"));
            _forms = new List<ContactForm>();

            for (int i = 0; i < _contactForms.Count; i++)
            {
                _forms.Add(new ContactForm(_contactForms.ElementAt(i),i));
            }

        }

    }

    public class ContactForm
    {
        protected IWebElement _contactForm;
        public IWebElement Name;
        public IWebElement Email;
        public IWebElement Message;
        public IWebElement Question;
        public IWebElement Answer;
        public IWebElement Submit;
        private static IWebElement _errorMessage;
        public IWebElement ErrorMessage
        {
            get => _errorMessage;
        }
        public ContactForm(IWebElement contactForm, int index)
        {
            try
            {
                _contactForm = contactForm.FindElement(By.ClassName($"et_pb_contact"));
                Name = _contactForm.FindElement(By.Id($"et_pb_contact_name_{index}"));
                Email = _contactForm.FindElement(By.Id($"et_pb_contact_email_{index}"));
                Message = _contactForm.FindElement(By.Id($"et_pb_contact_message_{index}"));
                Question = _contactForm.FindElement(By.ClassName("et_pb_contact_captcha_question"));
                Answer = _contactForm.FindElement(By.Name($"et_pb_contact_captcha_{index}"));
                Submit = _contactForm.FindElement(By.Name("et_builder_submit_button"));

            }
            catch (Exception e)
            {
            }

            _errorMessage = contactForm.FindElement(By.ClassName("et-pb-contact-message"));
        }

        public string AnswerTheQuestion()
        {
            var question = Question.Text;
            var x =Convert.ToInt32( question.Split("+").First());
            var y = Convert.ToInt32(question.Split("+").Last());
            return (x + y).ToString();
        }
        public RandomStuffSection FillContactFormAndSubmit(string name, string email, string message)
        {
            Name.SendKeys(name);
            Email.SendKeys(email);
            Message.SendKeys(message);
            Answer.SendKeys(AnswerTheQuestion());
            Submit.Click();
            return new RandomStuffSection();
        }

    }

    public class Toggle : PageContainer
    {
        protected IWebElement _toggle;
        public IWebElement Button;
        public IWebElement Inside;


        public Toggle()
        {
            _toggle = _sections.ElementAt(6).FindElement(By.ClassName("et_pb_toggle"));
            Button = _toggle.FindElement(By.Id("A_toggle"));
            Inside = _toggle.FindElement(By.ClassName("et_pb_toggle_content"));
        }
    }
}
