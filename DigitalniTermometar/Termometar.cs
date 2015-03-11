using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalniTermometar
{
    public interface SignalListener
    {
        void onSignal(bool highlow);
    }

    public interface Senzor
    {
        double dohvatiTrenutnuTemperaturu();
    }

    public class Termometar
    {
        private SignalListener signalListener;
        private Senzor senzor;

        public double granicnaTemperatura { get; set; }

        public Termometar(SignalListener signalListener, Senzor senzor)
        {
            this.signalListener = signalListener;
            this.senzor = senzor;
            this.granicnaTemperatura = 22.0;
        }

        public void provjeri()
        {
            double trenutnaTemperatura = senzor.dohvatiTrenutnuTemperaturu();
            if (trenutnaTemperatura < granicnaTemperatura)
            {
                signalListener.onSignal(true);
            }
            else
            {
                signalListener.onSignal(false);
            }
        }
    }
}

