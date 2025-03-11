using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestPlatform.Common.DataCollection;
using UltimateQAInfra;
using static UltimateQAInfra.ContactForm;

namespace Tests
{
    [TestFixture]
    public class Tests:SetUp
    {

        [Test]
        public void CountAllButtons()
        {
            ButtonsSection bt = new ButtonsSection();
            Assert.That(bt.Buttons.Count==12);
            Console.WriteLine(bt.Buttons.Count);

        }

        [Test]
        public void VerifyHrefOfFacebookButtons()
        {
            SocialsSection sc = new SocialsSection();

            foreach (var i in sc.Socials)
            {
                if (i.Name == "Follow on Facebook")
                {
                    Assert.That(i.Link == "https://www.facebook.com/Ultimateqa1/");
                }
            }

        }

        [Test]
        public void CountSocialsButtons()
        {
            SocialsSection sc = new SocialsSection();
            int facebookIcons = 0;
            int twitterIcons = 0;

            foreach (var i in sc.Socials)
            {
                if (i.Name == "Follow on Facebook")
                {
                    facebookIcons++;
                }
                if (i.Name == "Follow on Twitter")
                {
                    twitterIcons++;
                }

            }

            Assert.Multiple(() => 
            {
                Assert.That(facebookIcons == 5);
                Assert.That(twitterIcons == 5);
            });
        }


        [Test]
        public void RandomStuffTest()
        {
            RandomStuffSection rs = new RandomStuffSection();

            foreach (var form in rs.ContactForms)
            {
                form.FillContactFormAndSubmit("Tester", "tester@gmail.com", "Hello, World!");

            }
            foreach (var form in rs.ContactForms)
            {
                Assert.That(form.ErrorMessage.Text, Is.EqualTo("Thanks for contacting us"));

            }

        }


        [Test]
        public void ToggleTest()
        {
            Toggle tg = new Toggle();

            tg.Button.Click();

            Thread.Sleep(2000);

            Assert.Multiple(() =>
            {
                Assert.That(tg.Inside.Displayed);
                Assert.That(tg.Inside.Text.Equals("Inside of toggle"));
            });

        }
    }
}
