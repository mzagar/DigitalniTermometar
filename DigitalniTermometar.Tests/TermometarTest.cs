using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DigitalniTermometar;
using Moq;


namespace DigitalniTermometar.Tests
{
    [TestClass]
    public class TermometarTest
    {
        Mock<Senzor> senzor;
        Mock<SignalListener> signalListener;
        Termometar termometar;
        
        [TestInitialize]
        public void InicijalizirajPrijeTestova()
        {
            senzor = new Mock<Senzor>();
            signalListener = new Mock<SignalListener>();
            termometar = new Termometar(signalListener.Object, senzor.Object);
        }


        [TestMethod]
        public void Termometar_Ima_Granicnu_Temperaturu_Podesenu_Na_22()
        {
            Assert.AreEqual(22.0, termometar.granicnaTemperatura);
        }

        [TestMethod]
        public void Termometru_se_moze_mijenjati_granicna_temperatura()
        {
            termometar.granicnaTemperatura = 1;
            Assert.AreEqual(1, termometar.granicnaTemperatura);
            termometar.granicnaTemperatura = 100;
            Assert.AreEqual(100, termometar.granicnaTemperatura);
        }

        [TestMethod]
        public void Termometar_Mora_Dati_HIGH_signal_kada_temperatura_padne_ispod_granicne()
        {
            // Arrange
            senzor.Setup(s => s.dohvatiTrenutnuTemperaturu()).Returns(termometar.granicnaTemperatura - 1);
            
            // Act
            termometar.provjeri();
            
            // Assert
            signalListener.Verify(sl => sl.onSignal(true));
        }

        [TestMethod]
        public void Termometar_Mora_Dati_LOW_signal_kada_temperatura_predje_granicnu()
        {
            // Arrange
            Senzor senzor = Mock.Of<Senzor>(s => s.dohvatiTrenutnuTemperaturu() == 100);
            SignalListener signalListener = Mock.Of<SignalListener>();
            
            // Act
            new Termometar(signalListener, senzor).provjeri();
            
            // Assert
            Mock.Get(signalListener).Verify(sl => sl.onSignal(false));
        }
    }
}
