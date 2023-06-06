namespace Warehouse.Models
{
    public class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }


        //relacion con marca. Un producto pertenece a una marca
        public int MarcaId { get; set; }
        public Marca Marca { get; set; }




        public decimal Precio { get; set; }
    }
}