using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using TextConverter;
namespace Tests
{
    [TestClass]
    public class MarkdownTests
    {
        [TestMethod]
        //проверяем, что ключевые слова не будут читаться, а также то, что после них
        public void TestEmptyString()
        {
            Builder b1 = new MarkdownBuilder();
            Convertor director = new Convertor();
            //Проверка, что текст после неккоректных ключевых слов игнорируется
            string[] worktext = { "5343","43" };
            director.Construct(b1, worktext);
            Product p1 = b1.GetResult();
            Assert.AreEqual(p1.Show(), "");

            //Проверка, что после ordlist не читаются символы
            worktext = new string []{ "ordlist Это не будет читаться","1" };
            director.Construct(b1, worktext);
            p1 = b1.GetResult();
            Assert.AreEqual(p1.Show(), "1. 1");

            //Проверка, что после bullist не читаются символы
            worktext = new string[] { "bullist Это не будет читаться","4" };
            director.Construct(b1, worktext);
            p1 = b1.GetResult();
            Assert.AreEqual(p1.Show(), "* 4");

            //Проверка, что после bullist не читаются символы
            worktext = new string[] { "bullist Это не будет читаться", "4" };
            director.Construct(b1, worktext);
            p1 = b1.GetResult();
            Assert.AreEqual(p1.Show(), "* 4");

            //Проверка, что абзац начинающийся с неключевого слова игнорирутся
            worktext = new string[] { "4", "p 43","434" };
            director.Construct(b1, worktext);
            p1 = b1.GetResult();
            Assert.AreEqual(p1.Show(), "");

            //тест на абзац, между абзацами пробел и переход
            //это считается за один абзац, потому что в документации сказано, что
            //между абзацами должна быть пустая строка. здесь пробел
            worktext = new string[] { "p 4", " ", "434" };
            director.Construct(b1, worktext);
            p1 = b1.GetResult();
            Assert.AreEqual(p1.Show(), "4\n \n434");
        }

        //тесты на корректность обработки ключевых слов
        [TestMethod]
        public void BasicTest()
        {
            Builder b1 = new MarkdownBuilder();
            Convertor director = new Convertor();
            //Проверка, что на корректность обработки p
            string[] worktext = { "p 5", "43" };
            director.Construct(b1, worktext);
            Product p1 = b1.GetResult();
            Assert.AreEqual(p1.Show(), "5\n43");

            //Проверка, что на корректность обработки h1
            worktext = new string[] { "h1 Header"};
            director.Construct(b1, worktext);
            p1 = b1.GetResult();
            Assert.AreEqual(p1.Show(), "# Header #");

            //Проверка, что на корректность обработки h2
            worktext = new string[] { "h2 Header" };
            director.Construct(b1, worktext);
            p1 = b1.GetResult();
            Assert.AreEqual(p1.Show(), "## Header ##");

            //Проверка, что на корректность обработки h3
            worktext = new string[] { "h3 Header" };
            director.Construct(b1, worktext);
            p1 = b1.GetResult();
            Assert.AreEqual(p1.Show(), "### Header ###");

            //Проверка, что на корректность обработки ordlist
            worktext = new string[] { "ordlist ", "1","two","three" };
            director.Construct(b1, worktext);
            p1 = b1.GetResult();
            Assert.AreEqual(p1.Show(), "1. 1\n2. two\n3. three");

            //Проверка, что на корректность обработки bullist
            worktext = new string[] { "bullist", "one","two","three" };
            director.Construct(b1, worktext);
            p1 = b1.GetResult();
            Assert.AreEqual(p1.Show(), "* one\n* two\n* three");

            //Проверка, что все корректно, когда все вместе
            worktext = new string[] { "p 5", "43","","h1 H","","h2 H","","h3 H","","ordlist ",
                "1","","bullist","2" };
            director.Construct(b1, worktext);
            p1 = b1.GetResult();
            Assert.AreEqual(p1.Show(), "5\n43\n\n# H #\n\n## H ##\n\n### H ###\n\n1. 1\n\n* 2");

        }

        //тесты на проверку правильности обработки ввода при пересечении ключевых слов в абзаце
        [TestMethod] 
        public void IntersectedKeyWordsTest()
        {
            Builder b1 = new MarkdownBuilder();
            Convertor director = new Convertor();
            //Проверка на корректность обработки, если в абзац p попадет другой ключ
            string[] worktext = { "p 5", "h1 header" };
            director.Construct(b1, worktext);
            Product p1 = b1.GetResult();
            Assert.AreEqual(p1.Show(), "5\nh1 header");

            //Проверка на корректность обработки, если в h1 входит другой ключ
            //Мы начали Абзац с h1. Значит, по документации он действует до конца абзаца
            worktext = new string[] { "h1 Header","p 2","43","ordlist"};
            director.Construct(b1, worktext);
            p1 = b1.GetResult();
            Assert.AreEqual(p1.Show(), "# Header\np 2\n43\nordlist #");

            //Проверка на корректность обработки, если в h2 входит другой ключ
            //Мы начали Абзац с h2. Значит, по документации он действует до конца абзаца
            worktext = new string[] { "h2 Header", "p 2", "43", "ordlist" };
            director.Construct(b1, worktext);
            p1 = b1.GetResult();
            Assert.AreEqual(p1.Show(), "## Header\np 2\n43\nordlist ##");

            //Проверка на корректность обработки, если в h3 входит другой ключ
            //Мы начали Абзац с h3. Значит, по документации он действует до конца абзаца
            worktext = new string[] { "h3 Header", "p 2", "43", "ordlist" };
            director.Construct(b1, worktext);
            p1 = b1.GetResult();
            Assert.AreEqual(p1.Show(), "### Header\np 2\n43\nordlist ###");

            //Проверка на корректность обработки, если в ordlist входит другой ключ
            worktext = new string[] { "ordlist","p 42","h2 4","bullist" };
            director.Construct(b1, worktext);
            p1 = b1.GetResult();
            Assert.AreEqual(p1.Show(), "1. p 42\n2. h2 4\n3. bullist");

            //Проверка на корректность обработки, если в bullist входит другой ключ
            worktext = new string[] { "bullist", "p 42", "h2 4", "ordlist" };
            director.Construct(b1, worktext);
            p1 = b1.GetResult();
            Assert.AreEqual(p1.Show(), "* p 42\n* h2 4\n* ordlist");
        }

        //Тесты на проверку, если у нас несколько абзацев 
        [TestMethod]
        public void SeveralParagraphsTest()
        {
            Builder b1 = new MarkdownBuilder();
            Convertor director = new Convertor();
            //Проверка на корректность обработки p
            string[] worktext = { "p 5", "43","","p 42" };
            director.Construct(b1, worktext);
            Product p1 = b1.GetResult();
            Assert.AreEqual(p1.Show(), "5\n43\n\n42");

            //Проверка на корректность обработки h1,h2,h3
            worktext = new string[] { "h1 Header","","h3 h2 fsda","","h2 fa"};
            director.Construct(b1, worktext);
            p1 = b1.GetResult();
            Assert.AreEqual(p1.Show(), "# Header #\n\n### h2 fsda ###\n\n## fa ##");

            //Проверка на корректность обработки ordlist и bullist
            worktext = new string[] { "ordlist af","one","","bullist","42","","ordlist","two"};
            director.Construct(b1, worktext);
            p1 = b1.GetResult();
            Assert.AreEqual(p1.Show(), "1. one\n\n* 42\n\n1. two");

            //Проверка на корректность если мы разделяем абзац несколькими строчками
            worktext = new string[] { "p 42","","","","h1 4","","","p 42234" };
            director.Construct(b1, worktext);
            p1 = b1.GetResult();
            Assert.AreEqual(p1.Show(), "42\n\n# 4 #\n\n42234");
        }

        //Тест из примера на мудле
        [TestMethod]
        public void FinalTest()
        {
            Builder b1 = new MarkdownBuilder();
            Convertor director = new Convertor();
            
            string[] worktext = { "h1 Заголовок 1","","p Привет, мир!","","p Внимание!","Это нумерованный список:",""
                    ,"ordlist эти символы игнорируются","пункт 1;","пункт 2;","пункт 3.","","h2 Заголовок 2","",
                "Этот текст игнорируется, так вот!","","p А это маркированный список:",
                "","bullist","пункт 1;","пункт 2.","","h1 Конец!"};
            director.Construct(b1, worktext);
            Product p1 = b1.GetResult();
            Assert.AreEqual(p1.Show(),"# Заголовок 1 #\n\nПривет, мир!\n\nВнимание!\nЭто нумерованный список:\n\n1. пункт 1;\n"
                +"2. пункт 2;\n3. пункт 3.\n\n## Заголовок 2 ##\n\nА это маркированный список:\n\n* пункт 1;\n* пункт 2.\n\n# Конец! #");
        }
    }
}
