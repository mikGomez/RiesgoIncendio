using RiesgoIncendio.Views;

namespace TestRiesgoIncendio
{
    public class UnitTest1
    {
        [Fact]
        public void TestValoresTipicos()
        {
            double temperatura = 9.7;
            double humedadRelativa = 68;
            double resultadoEsperado = 4;
            double resultadoCalculado = MainWindow.CalcularCBI(temperatura, humedadRelativa);
            Assert.Equal(resultadoEsperado, resultadoCalculado);
        }
        [Fact]
        public void HumedadPositiva_DeberiaRetornarTrue()
        {
            double humedad = 30;

            bool resultado = MainWindow.ValidarHumedad(humedad);

            Assert.True(resultado);
        }
    }
}