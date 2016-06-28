using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using TextConverter;
namespace UnitTestProject1
{
    [TestClass]
    public class HTMLTests
    {
        [TestMethod]
        //проверяем, что ключевые слова не будут читаться, а также то, что после них
        public void TestEmptyString()
        {
            Builder b1 = new HTMLBuilder();
            Convertor director = new Convertor();
            //Проверка, что текст после неккоректных ключевых слов игнорируется
            string[] worktext = { "777", "777" };
            director.Construct(b1, worktext);
            Product p1 = b1.GetResult();
            Assert.AreEqual(p1.Show(), "");

            //Проверка, что после ordlist не читаются символы
            worktext = new string[] { "ordlist ignore ignore ignore", "1" };
            director.Construct(b1, worktext);
            p1 = b1.GetResult();
            Assert.AreEqual(p1.Show(), "<ol>" + '\n' + "<li>" + "1" + " </li>" + '\n' + "</ol>" + '\n' + '\n');

      
            //Проверка, что абзац начинающийся с неключевого слова игнорирутся
            Builder b2 = new HTMLBuilder();
            worktext = new string[] { "4", "5", "777" };
            director.Construct(b2, worktext);
            Product p2 = b2.GetResult();
            Assert.AreEqual(p2.Show(), "");

            /*//тест на абзац, между абзацами пробел и переход
            //это считается за один абзац, потому что в документации сказано, что
            //между абзацами должна быть пустая строка. здесь пробел
            worktext = new string[] { "p 4", " ", "434" };
            director.Construct(b1, worktext);
            p1 = b1.GetResult();
            Assert.AreEqual(p1.Show(), "4\n \n434");*/
        }
    }
}
