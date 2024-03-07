using RiesgoIncendio.Views;

namespace TestRiesgoIncendio
{
    public class UnitTest1
    {
        [Fact]
        public void TestRiesgo()
        {
            double temperatura = 9.7;
            double humedadRelativa = 68;
            double resultadoEsperado = 4;
            double resultadoCalculado = MainWindow.CalcularCBI(temperatura, humedadRelativa);
            Assert.Equal(resultadoEsperado, resultadoCalculado);
        }
        [Fact]
        public void TestTemperaturaBaja()
        {
            double temperatura = 15.8;
            double humedadRelativa = 47;
            double resultadoEsperado = 22;
            double resultadoCalculado = MainWindow.CalcularCBI(temperatura, humedadRelativa);
            Assert.Equal(resultadoEsperado, resultadoCalculado);
        }

        [Fact]
        public void HumedadPositiva()
        {
            double humedad = 30;

            bool resultado = MainWindow.ValidarHumedad(humedad);

            Assert.True(resultado);
        }
        [Fact]
        public void HumedadNegativa()
        {
            double humedad = -1;

            bool resultado = MainWindow.ValidarHumedad(humedad);

            Assert.False(resultado);
        }
    }
}