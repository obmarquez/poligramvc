using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using poligramvc.Models;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Web.Services.Protocols;

namespace poligramvc.Controllers
{
    public class ImpresionesController : Controller
    {
        float[] widthsTitulosGenerales = new float[] { 1f };

        // GET: Impresiones
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult printPoligrafia(int sIdH, int sIdHP)
        {
            var datosPoligrafia = RepInvPol.getRepInvPol(sIdH, sIdHP).FirstOrDefault();

            MemoryStream ms = new MemoryStream();

            Document documentInvPol = new Document(PageSize.LETTER, 30f, 20f, 50f, 40f);
            PdfWriter pwInvPol = PdfWriter.GetInstance(documentInvPol, ms);

            pwInvPol.PageEvent = new HeaderFooter();

            documentInvPol.Open();        
            
            string imagenPath = @"C:/inetpub/wwwroot/fotoUser/gobedohor.png";                           //--para verlo en mi el servidor
            //string imagenPath = @"C:\Net 2012\psicologiamvc\psicologiamvc\Content\img\gobedohor.png";   //--para verlo en mi el proyecto
            Image imagen = Image.GetInstance(imagenPath);
            imagen.ScalePercent(35f);
            imagen.SetAbsolutePosition(30f, 725f);
            documentInvPol.Add(imagen);

            #region Datos Generales
            Chunk chunk = new Chunk();
            Paragraph derecha = new Paragraph();
            derecha.Alignment = Element.ALIGN_RIGHT;

            //------------------------------------ ver imagen desde base de datos
            Byte[] bytes = (Byte[])datosPoligrafia.foto;
            iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(bytes);
            img.ScalePercent(50f);
            //img.SetAbsolutePosition(500f, 600f); //ORIGINAL
            img.SetAbsolutePosition(500f, 580f);
            derecha.Add(img);

            //derecha.Add(Chunk.NEWLINE);
            derecha.Add(Chunk.NEWLINE);
            derecha.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            derecha.Add("Codigo Evaluado: ");
            derecha.Add(Chunk.TABBING);
            derecha.Font = FontFactory.GetFont("Arial", 10, Font.BOLD);
            //derecha.Add(DateTime.Now.ToShortDateString());
            derecha.Add(datosPoligrafia.codigoevaluado);

            documentInvPol.Add(derecha);
            #endregion

            #region Titulo Datos Generales
            PdfPTable tableTituloDatoGeneral = new PdfPTable(1);
            tableTituloDatoGeneral.TotalWidth = 560f;
            tableTituloDatoGeneral.LockedWidth = true;

            tableTituloDatoGeneral.SetWidths(widthsTitulosGenerales);
            tableTituloDatoGeneral.HorizontalAlignment = 0;
            tableTituloDatoGeneral.SpacingBefore = 10f;
            tableTituloDatoGeneral.SpacingAfter = 10f;

            PdfPCell cellTituloTituloDato = new PdfPCell(new Phrase("DATOS GENERALES", new Font(Font.FontFamily.HELVETICA, 10f, Font.BOLDITALIC)));
            cellTituloTituloDato.HorizontalAlignment = 1;
            cellTituloTituloDato.BackgroundColor = new iTextSharp.text.BaseColor(234, 236, 238);
            cellTituloTituloDato.BorderColor = new iTextSharp.text.BaseColor(0, 0, 0);
            cellTituloTituloDato.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
            tableTituloDatoGeneral.AddCell(cellTituloTituloDato);

            documentInvPol.Add(tableTituloDatoGeneral);
            #endregion

            #region Datos Generales
            Paragraph datosGenerales = new Paragraph();
            datosGenerales.Alignment = Element.ALIGN_LEFT;
            datosGenerales.Font = FontFactory.GetFont("Arial", 10, Font.BOLD);
            datosGenerales.Add("Nombre: ");
            datosGenerales.Add(Chunk.TABBING);
            datosGenerales.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            datosGenerales.Add(datosPoligrafia.evaluado);
            datosGenerales.Add(Chunk.NEWLINE);
            datosGenerales.Font = FontFactory.GetFont("Arial", 10, Font.BOLD);
            datosGenerales.Add("Edad: ");
            datosGenerales.Add(Chunk.TABBING);
            datosGenerales.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            datosGenerales.Add(datosPoligrafia.edad);
            datosGenerales.Add(Chunk.TABBING);
            datosGenerales.Add(Chunk.TABBING);
            datosGenerales.Font = FontFactory.GetFont("Arial", 10, Font.BOLD);
            datosGenerales.Add("Genero: ");
            datosGenerales.Add(Chunk.TABBING);
            datosGenerales.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            datosGenerales.Add(datosPoligrafia.sexo);
            datosGenerales.Add(Chunk.NEWLINE);
            datosGenerales.Font = FontFactory.GetFont("Arial", 10, Font.BOLD);
            datosGenerales.Add("Escolaridad: ");
            datosGenerales.Add(Chunk.TABBING);
            datosGenerales.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            datosGenerales.Add(datosPoligrafia.obsest);
            datosGenerales.Add(Chunk.NEWLINE);
            datosGenerales.Font = FontFactory.GetFont("Arial", 10, Font.BOLD);
            datosGenerales.Add("RFC: ");
            datosGenerales.Add(Chunk.TABBING);
            datosGenerales.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            datosGenerales.Add(datosPoligrafia.rfc);
            datosGenerales.Add(Chunk.TABBING);
            datosGenerales.Add(Chunk.TABBING);
            datosGenerales.Font = FontFactory.GetFont("Arial", 10, Font.BOLD);
            datosGenerales.Add("CURP: ");
            datosGenerales.Add(Chunk.TABBING);
            datosGenerales.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            datosGenerales.Add(datosPoligrafia.curp);
            datosGenerales.Add(Chunk.NEWLINE);
            datosGenerales.Font = FontFactory.GetFont("Arial", 10, Font.BOLD);
            datosGenerales.Add("Domicilio: ");
            datosGenerales.Add(Chunk.TABBING);
            datosGenerales.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            datosGenerales.Add(datosPoligrafia.domicilio);
            datosGenerales.Add(Chunk.NEWLINE);
            datosGenerales.Font = FontFactory.GetFont("Arial", 10, Font.BOLD);
            datosGenerales.Add("Motivo de Evaluacion: ");
            datosGenerales.Add(Chunk.TABBING);
            datosGenerales.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            datosGenerales.Add(datosPoligrafia.cevaluacion);
            datosGenerales.Add(Chunk.TABBING); datosGenerales.Add(Chunk.TABBING);
            datosGenerales.Font = FontFactory.GetFont("Arial", 10, Font.BOLD);
            datosGenerales.Add("Fecha de ingreso: ");
            datosGenerales.Add(Chunk.TABBING);
            datosGenerales.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            datosGenerales.Add(datosPoligrafia.fechaAlta);
            datosGenerales.Add(Chunk.NEWLINE);
            datosGenerales.Font = FontFactory.GetFont("Arial", 10, Font.BOLD);
            datosGenerales.Add("Dependencia: ");
            datosGenerales.Add(Chunk.TABBING);
            datosGenerales.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            datosGenerales.Add(datosPoligrafia.dependencia);
            datosGenerales.Add(Chunk.NEWLINE);
            datosGenerales.Font = FontFactory.GetFont("Arial", 10, Font.BOLD);
            datosGenerales.Add("Sub Dependencia: ");
            datosGenerales.Add(Chunk.TABBING);
            datosGenerales.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            datosGenerales.Add(datosPoligrafia.subDependencia);
            datosGenerales.Add(Chunk.NEWLINE);
            datosGenerales.Font = FontFactory.GetFont("Arial", 10, Font.BOLD);
            datosGenerales.Add("Puesto: ");
            datosGenerales.Add(Chunk.TABBING);
            datosGenerales.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            datosGenerales.Add(datosPoligrafia.puesto);
            datosGenerales.Add(Chunk.NEWLINE);
            datosGenerales.Font = FontFactory.GetFont("Arial", 10, Font.BOLD);
            datosGenerales.Add("Categoria del puesto: ");
            datosGenerales.Add(Chunk.TABBING);
            datosGenerales.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            datosGenerales.Add(datosPoligrafia.categoriaPuesto);
            datosGenerales.Add(Chunk.NEWLINE);
            datosGenerales.Font = FontFactory.GetFont("Arial", 10, Font.BOLD);
            datosGenerales.Add("Funcion declarada: ");
            datosGenerales.Add(Chunk.TABBING);
            datosGenerales.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            datosGenerales.Add(datosPoligrafia.funcion);
            datosGenerales.Add(Chunk.NEWLINE);
            datosGenerales.Font = FontFactory.GetFont("Arial", 10, Font.BOLD);
            datosGenerales.Add("Funcion institucional: ");
            datosGenerales.Add(Chunk.TABBING);
            datosGenerales.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            datosGenerales.Add(datosPoligrafia.funcionInstitucional);
            datosGenerales.Add(Chunk.NEWLINE);
            datosGenerales.Font = FontFactory.GetFont("Arial", 10, Font.BOLD);
            datosGenerales.Add("Area de adscripcion: ");
            datosGenerales.Add(Chunk.TABBING);
            datosGenerales.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            datosGenerales.Add(datosPoligrafia.cAdscripcion);
            datosGenerales.Add(Chunk.NEWLINE);
            datosGenerales.Font = FontFactory.GetFont("Arial", 10, Font.BOLD);
            datosGenerales.Add("Comisionado: ");
            datosGenerales.Add(Chunk.TABBING);
            datosGenerales.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            datosGenerales.Add(datosPoligrafia.comision);
            datosGenerales.Add(Chunk.NEWLINE);
            datosGenerales.Font = FontFactory.GetFont("Arial", 10, Font.BOLD);
            datosGenerales.Add("Fecha de evaluacion: ");
            datosGenerales.Add(Chunk.TABBING);
            datosGenerales.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            datosGenerales.Add(datosPoligrafia.fEvalpol);
            datosGenerales.Add(Chunk.NEWLINE);

            documentInvPol.Add(datosGenerales);
            #endregion

            #region Trayectoria laboral
            PdfPTable tableTrayectoria = new PdfPTable(1);
            tableTrayectoria.TotalWidth = 560f;
            tableTrayectoria.LockedWidth = true;

            tableTrayectoria.SetWidths(widthsTitulosGenerales);
            tableTrayectoria.HorizontalAlignment = 0;
            tableTrayectoria.SpacingBefore = 20f;
            tableTrayectoria.SpacingAfter = 10f;

            PdfPCell cellTrayectoria = new PdfPCell(new Phrase("TRAYECTORIA LABORAL", new Font(Font.FontFamily.HELVETICA, 10f, Font.BOLDITALIC)));
            cellTrayectoria.HorizontalAlignment = 1;
            cellTrayectoria.BackgroundColor = new iTextSharp.text.BaseColor(234, 236, 238);
            cellTrayectoria.BorderColor = new iTextSharp.text.BaseColor(0, 0, 0);
            cellTrayectoria.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
            tableTrayectoria.AddCell(cellTrayectoria);

            documentInvPol.Add(tableTrayectoria);
            #endregion

            #region Contenido Trayectoria
            Paragraph trayectoria = new Paragraph();
            trayectoria.Alignment = Element.ALIGN_JUSTIFIED;

            trayectoria.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            trayectoria.Add(datosPoligrafia.cLaboral);

            documentInvPol.Add(trayectoria);
            #endregion

            #region Admisiones
            PdfPTable tableAdmisiones = new PdfPTable(1);
            tableAdmisiones.TotalWidth = 560f;
            tableAdmisiones.LockedWidth = true;

            tableAdmisiones.SetWidths(widthsTitulosGenerales);
            tableAdmisiones.HorizontalAlignment = 0;
            tableAdmisiones.SpacingBefore = 20f;
            tableAdmisiones.SpacingAfter = 10f;

            PdfPCell cellAdmisiones = new PdfPCell(new Phrase("ADMISIONES", new Font(Font.FontFamily.HELVETICA, 10f, Font.BOLDITALIC)));
            cellAdmisiones.HorizontalAlignment = 1;
            cellAdmisiones.BackgroundColor = new iTextSharp.text.BaseColor(234, 236, 238);
            cellAdmisiones.BorderColor = new iTextSharp.text.BaseColor(0, 0, 0);
            cellAdmisiones.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
            tableAdmisiones.AddCell(cellAdmisiones);

            documentInvPol.Add(tableAdmisiones);
            #endregion

            #region Contenido Admisiones
            Paragraph admisiones = new Paragraph();
            admisiones.Alignment = Element.ALIGN_JUSTIFIED;

            admisiones.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            admisiones.Add(datosPoligrafia.cAdmisiones);

            documentInvPol.Add(admisiones);
            #endregion

            #region Observaciones
            PdfPTable tableObservaciones = new PdfPTable(1);
            tableObservaciones.TotalWidth = 560f;
            tableObservaciones.LockedWidth = true;

            tableObservaciones.SetWidths(widthsTitulosGenerales);
            tableObservaciones.HorizontalAlignment = 0;
            tableObservaciones.SpacingBefore = 20f;
            tableObservaciones.SpacingAfter = 10f;

            PdfPCell cellObservacion = new PdfPCell(new Phrase("OBSERVACIONES", new Font(Font.FontFamily.HELVETICA, 10f, Font.BOLDITALIC)));
            cellObservacion.HorizontalAlignment = 1;
            cellObservacion.BackgroundColor = new iTextSharp.text.BaseColor(234, 236, 238);
            cellObservacion.BorderColor = new iTextSharp.text.BaseColor(0, 0, 0);
            cellObservacion.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
            tableObservaciones.AddCell(cellObservacion);

            documentInvPol.Add(tableObservaciones);
            #endregion

            #region Contenido Observaciones
            Paragraph observaciones = new Paragraph();
            observaciones.Alignment = Element.ALIGN_JUSTIFIED;

            observaciones.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            observaciones.Add(datosPoligrafia.cObservaciones);

            documentInvPol.Add(observaciones);
            #endregion

            #region Notas
            PdfPTable tableNotas = new PdfPTable(1);
            tableNotas.TotalWidth = 560f;
            tableNotas.LockedWidth = true;

            tableNotas.SetWidths(widthsTitulosGenerales);
            tableNotas.HorizontalAlignment = 0;
            tableNotas.SpacingBefore = 20f;
            tableNotas.SpacingAfter = 10f;

            PdfPCell cellNotas = new PdfPCell(new Phrase("NOTAS", new Font(Font.FontFamily.HELVETICA, 10f, Font.BOLDITALIC)));
            cellNotas.HorizontalAlignment = 1;
            cellNotas.BackgroundColor = new iTextSharp.text.BaseColor(234, 236, 238);
            cellNotas.BorderColor = new iTextSharp.text.BaseColor(0, 0, 0);
            cellNotas.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
            tableNotas.AddCell(cellNotas);

            documentInvPol.Add(tableNotas);
            #endregion

            #region Contenido Notas
            Paragraph notas = new Paragraph();
            notas.Alignment = Element.ALIGN_JUSTIFIED;

            notas.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            notas.Add(datosPoligrafia.cNota);

            documentInvPol.Add(notas);
            #endregion

            #region Sintesis tecnica
            PdfPTable tableSintesis = new PdfPTable(1);
            tableSintesis.TotalWidth = 560f;
            tableSintesis.LockedWidth = true;

            tableSintesis.SetWidths(widthsTitulosGenerales);
            tableSintesis.HorizontalAlignment = 0;
            tableSintesis.SpacingBefore = 20f;
            tableSintesis.SpacingAfter = 10f;

            PdfPCell cellSintesis = new PdfPCell(new Phrase("SINTESIS TECNICA", new Font(Font.FontFamily.HELVETICA, 10f, Font.BOLDITALIC)));
            cellSintesis.HorizontalAlignment = 1;
            cellSintesis.BackgroundColor = new iTextSharp.text.BaseColor(234, 236, 238);
            cellSintesis.BorderColor = new iTextSharp.text.BaseColor(0, 0, 0);
            cellSintesis.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
            tableSintesis.AddCell(cellSintesis);

            documentInvPol.Add(tableSintesis);
            #endregion

            #region Contenido Sintesis
            Paragraph sintesis = new Paragraph();
            sintesis.Alignment = Element.ALIGN_JUSTIFIED;

            sintesis.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            sintesis.Add(datosPoligrafia.cSintesis);

            documentInvPol.Add(sintesis);
            #endregion

            #region Analisis tecnico
            PdfPTable tableAnalisis = new PdfPTable(1);
            tableAnalisis.TotalWidth = 560f;
            tableAnalisis.LockedWidth = true;

            tableAnalisis.SetWidths(widthsTitulosGenerales);
            tableAnalisis.HorizontalAlignment = 0;
            tableAnalisis.SpacingBefore = 20f;
            tableAnalisis.SpacingAfter = 10f;

            PdfPCell cellAnalisis = new PdfPCell(new Phrase("ANALISIS TECNICO", new Font(Font.FontFamily.HELVETICA, 10f, Font.BOLDITALIC)));
            cellAnalisis.HorizontalAlignment = 1;
            cellAnalisis.BackgroundColor = new iTextSharp.text.BaseColor(234, 236, 238);
            cellAnalisis.BorderColor = new iTextSharp.text.BaseColor(0, 0, 0);
            cellAnalisis.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
            tableAnalisis.AddCell(cellAnalisis);

            documentInvPol.Add(tableAnalisis);
            #endregion

            #region Contenido Analisis
            Paragraph analisis = new Paragraph();
            analisis.Alignment = Element.ALIGN_JUSTIFIED;

            analisis.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            analisis.Add(datosPoligrafia.cAnalisis);

            documentInvPol.Add(analisis);
            #endregion

            #region Diagnostico
            PdfPTable tableDx = new PdfPTable(1);
            tableDx.TotalWidth = 560f;
            tableDx.LockedWidth = true;

            tableDx.SetWidths(widthsTitulosGenerales);
            tableDx.HorizontalAlignment = 0;
            tableDx.SpacingBefore = 20f;
            tableDx.SpacingAfter = 10f;

            PdfPCell celldX = new PdfPCell(new Phrase("DIAGNOSTICO", new Font(Font.FontFamily.HELVETICA, 10f, Font.BOLDITALIC)));
            celldX.HorizontalAlignment = 1;
            celldX.BackgroundColor = new iTextSharp.text.BaseColor(234, 236, 238);
            celldX.BorderColor = new iTextSharp.text.BaseColor(0, 0, 0);
            celldX.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
            tableDx.AddCell(celldX);

            documentInvPol.Add(tableDx);
            #endregion

            #region Contenido Dx
            Paragraph dx = new Paragraph();
            //dx.Alignment = Element.ALIGN_JUSTIFIED;
            dx.Alignment = Element.ALIGN_CENTER;

            dx.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            dx.Add("DX :"); dx.Add(Chunk.TABBING); dx.Add(datosPoligrafia.dx);

            dx.Add(Chunk.NEWLINE); dx.Add(Chunk.NEWLINE); dx.Add(Chunk.NEWLINE); dx.Add(Chunk.NEWLINE);

            documentInvPol.Add(dx);
            #endregion

            #region Firmas
            PdfPTable tablaFirmas = new PdfPTable(3);
            tablaFirmas.TotalWidth = 560;
            tablaFirmas.LockedWidth = true;

            float[] widths = new float[] { 1f, 1f, 1f };
            tablaFirmas.SetWidths(widths);
            tablaFirmas.HorizontalAlignment = 0;
            tablaFirmas.SpacingAfter = 20f;
            tablaFirmas.SpacingAfter = 30f;
            tablaFirmas.DefaultCell.Border = 0; //--------------------

            PdfPCell cel1a = new PdfPCell(new Phrase(datosPoligrafia.evaluo, new Font(Font.FontFamily.HELVETICA, 10f, Font.NORMAL)));
            cel1a.HorizontalAlignment = Element.ALIGN_CENTER;
            cel1a.Border = 0;

            PdfPCell cel1b = new PdfPCell(new Phrase(datosPoligrafia.supervisor, new Font(Font.FontFamily.HELVETICA, 10f, Font.NORMAL)));
            cel1b.HorizontalAlignment = Element.ALIGN_CENTER;
            cel1b.Border = 0;

            PdfPCell cel1c = new PdfPCell(new Phrase("AP 538", new Font(Font.FontFamily.HELVETICA, 10f, Font.NORMAL)));
            cel1c.HorizontalAlignment = Element.ALIGN_CENTER;
            cel1c.Border = 0;

            PdfPCell cel2a = new PdfPCell(new Phrase("Evaluo", new Font(Font.FontFamily.HELVETICA, 10f, Font.NORMAL)));
            cel2a.HorizontalAlignment = Element.ALIGN_CENTER;
            cel2a.Border = 0;
            //cel2a.Border = 1;

            PdfPCell cel2b = new PdfPCell(new Phrase("Superviso", new Font(Font.FontFamily.HELVETICA, 10f, Font.NORMAL)));
            cel2b.HorizontalAlignment = Element.ALIGN_CENTER;
            cel2b.Border = 0;
            //cel2a.Border = 1;

            PdfPCell cel2c = new PdfPCell(new Phrase("Válido", new Font(Font.FontFamily.HELVETICA, 10f, Font.NORMAL)));
            cel2c.HorizontalAlignment = Element.ALIGN_CENTER;
            cel2c.Border = 0;
            //cel2c.Border = 1;

            tablaFirmas.AddCell(cel1a);
            tablaFirmas.AddCell(cel1b);
            tablaFirmas.AddCell(cel1c);

            tablaFirmas.AddCell(cel2a);
            tablaFirmas.AddCell(cel2b);
            tablaFirmas.AddCell(cel2c);

            documentInvPol.Add(tablaFirmas);

            #endregion

            documentInvPol.Close();

            byte[] bytesStream = ms.ToArray();

            ms = new MemoryStream();
            ms.Write(bytesStream, 0, bytesStream.Length);

            ms.Position = 0;

            return new FileStreamResult(ms, "application/pdf");
        }

        public ActionResult printIndice(int idRep)
        {
            var datosIndice = Indice.datosRepIndice(idRep).FirstOrDefault();
            MemoryStream ms = new MemoryStream();
            Document docIndRep = new Document(PageSize.LETTER, 30f, 20f, 50f, 40f);
            PdfWriter pwRepInd = PdfWriter.GetInstance(docIndRep, ms);
            pwRepInd.PageEvent = new HeaderFooterIndice();
            docIndRep.Open();

            string imagenPath = @"C:/inetpub/wwwroot/fotoUser/gobedohor.png";                           //--para verlo en mi el servidor
            //string imagenPath = @"C:\Net 2012\psicologiamvc\psicologiamvc\Content\img\gobedohor.png";   //--para verlo en mi el proyecto
            Image imagen = Image.GetInstance(imagenPath);
            imagen.ScalePercent(35f);
            imagen.SetAbsolutePosition(30f, 725f);
            docIndRep.Add(imagen);

            #region Datos Generales
            Chunk chunk = new Chunk();
            Paragraph DGeneral = new Paragraph();
            DGeneral.Add(Chunk.NEWLINE); DGeneral.Add(Chunk.NEWLINE);
            DGeneral.Alignment = Element.ALIGN_LEFT;
            DGeneral.Font = FontFactory.GetFont("Arial", 10, Font.BOLD);
            DGeneral.Add("Nombre del Evaluado: ");
            DGeneral.Add(Chunk.TABBING);
            DGeneral.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            DGeneral.Add(datosIndice.evaluado);
            DGeneral.Add(Chunk.TABBING); DGeneral.Add(Chunk.TABBING); DGeneral.Add(Chunk.TABBING); DGeneral.Add(Chunk.TABBING); DGeneral.Add(Chunk.TABBING); DGeneral.Add(Chunk.TABBING); DGeneral.Add(Chunk.TABBING);
            DGeneral.Add(datosIndice.serial.Substring(0, 6));
            DGeneral.Add(Chunk.NEWLINE);
            DGeneral.Font = FontFactory.GetFont("Arial", 10, Font.BOLD);
            DGeneral.Add("R.F.C. : ");
            DGeneral.Add(Chunk.TABBING);
            DGeneral.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            DGeneral.Add(datosIndice.rfc);
            DGeneral.Add(Chunk.TABBING); DGeneral.Add(Chunk.TABBING); DGeneral.Add(Chunk.TABBING); DGeneral.Add(Chunk.TABBING); DGeneral.Add(Chunk.TABBING); DGeneral.Add(Chunk.TABBING); DGeneral.Add(Chunk.TABBING); DGeneral.Add(Chunk.TABBING);
            DGeneral.Font = FontFactory.GetFont("Arial", 10, Font.BOLD);
            DGeneral.Add("Fecha : ");
            DGeneral.Add(Chunk.TABBING);
            DGeneral.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            DGeneral.Add(datosIndice.fecha);
            
            docIndRep.Add(DGeneral);
            #endregion

            #region Indice
            Paragraph DocIndice = new Paragraph();
            DocIndice.Add(Chunk.NEWLINE); DocIndice.Add(Chunk.NEWLINE);
            DocIndice.Add(Chunk.TABBING); 
            DocIndice.Font = FontFactory.GetFont("Arial", 10, Font.BOLD);
            DocIndice.Add("Especifiación");
            DocIndice.Add(Chunk.TABBING); DocIndice.Add(Chunk.TABBING); DocIndice.Add(Chunk.TABBING); DocIndice.Add(Chunk.TABBING); DocIndice.Add(Chunk.TABBING); DocIndice.Add(Chunk.TABBING); DocIndice.Add(Chunk.TABBING);
            DocIndice.Add("No. de hojas");

            DocIndice.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            DocIndice.Add(Chunk.NEWLINE); DocIndice.Add(Chunk.NEWLINE);
            DocIndice.Add(Chunk.TABBING);
            DocIndice.Add("Reporte");
            DocIndice.Add(Chunk.TABBING); DocIndice.Add(Chunk.TABBING); DocIndice.Add(Chunk.TABBING); DocIndice.Add(Chunk.TABBING); DocIndice.Add(Chunk.TABBING); DocIndice.Add(Chunk.TABBING); DocIndice.Add(Chunk.TABBING);
            DocIndice.Add(Chunk.TABBING); DocIndice.Add(Chunk.TABBING);
            DocIndice.Add(datosIndice.nreporte.ToString());

            DocIndice.Add(Chunk.NEWLINE); DocIndice.Add(Chunk.NEWLINE);
            DocIndice.Add(Chunk.TABBING);
            DocIndice.Add("Datos generales / Autorización de Temas");
            DocIndice.Add(Chunk.TABBING); DocIndice.Add(Chunk.TABBING); DocIndice.Add(Chunk.TABBING); DocIndice.Add(Chunk.TABBING);
            DocIndice.Add(datosIndice.datosgenerales.ToString());

            DocIndice.Add(Chunk.NEWLINE); DocIndice.Add(Chunk.NEWLINE);
            DocIndice.Add(Chunk.TABBING);
            DocIndice.Add("Carta autorización / Manifestación de Protección de Datos");
            DocIndice.Add(Chunk.TABBING); DocIndice.Add(Chunk.TABBING); 
            DocIndice.Add(datosIndice.carta.ToString());

            DocIndice.Add(Chunk.NEWLINE); DocIndice.Add(Chunk.NEWLINE);
            DocIndice.Add(Chunk.TABBING);
            DocIndice.Add("Antecedentes personales / Comentarios");
            DocIndice.Add(Chunk.TABBING); DocIndice.Add(Chunk.TABBING); DocIndice.Add(Chunk.TABBING); DocIndice.Add(Chunk.TABBING); DocIndice.Add(Chunk.TABBING);
            DocIndice.Add(datosIndice.personales.ToString());

            DocIndice.Add(Chunk.NEWLINE); DocIndice.Add(Chunk.NEWLINE);
            DocIndice.Add(Chunk.TABBING);
            DocIndice.Add("Entrevista");
            DocIndice.Add(Chunk.TABBING); DocIndice.Add(Chunk.TABBING); DocIndice.Add(Chunk.TABBING); DocIndice.Add(Chunk.TABBING); DocIndice.Add(Chunk.TABBING); DocIndice.Add(Chunk.TABBING); DocIndice.Add(Chunk.TABBING);
            DocIndice.Add(Chunk.TABBING); 
            DocIndice.Add(datosIndice.entrevista.ToString());

            DocIndice.Add(Chunk.NEWLINE); DocIndice.Add(Chunk.NEWLINE);
            DocIndice.Add(Chunk.TABBING);
            DocIndice.Add("Preguntas");
            DocIndice.Add(Chunk.TABBING); DocIndice.Add(Chunk.TABBING); DocIndice.Add(Chunk.TABBING); DocIndice.Add(Chunk.TABBING); DocIndice.Add(Chunk.TABBING); DocIndice.Add(Chunk.TABBING); DocIndice.Add(Chunk.TABBING);
            DocIndice.Add(Chunk.TABBING);
            DocIndice.Add(datosIndice.preguntas.ToString());

            DocIndice.Add(Chunk.NEWLINE); DocIndice.Add(Chunk.NEWLINE);
            DocIndice.Add(Chunk.TABBING);
            DocIndice.Add("Análisis numérico");
            DocIndice.Add(Chunk.TABBING); DocIndice.Add(Chunk.TABBING); DocIndice.Add(Chunk.TABBING); DocIndice.Add(Chunk.TABBING); DocIndice.Add(Chunk.TABBING); DocIndice.Add(Chunk.TABBING); DocIndice.Add(Chunk.TABBING);
            DocIndice.Add(datosIndice.analisis.ToString());

            DocIndice.Add(Chunk.NEWLINE); DocIndice.Add(Chunk.NEWLINE);
            DocIndice.Add(Chunk.TABBING);
            DocIndice.Add("Gráficos");
            DocIndice.Add(Chunk.TABBING); DocIndice.Add(Chunk.TABBING); DocIndice.Add(Chunk.TABBING); DocIndice.Add(Chunk.TABBING); DocIndice.Add(Chunk.TABBING); DocIndice.Add(Chunk.TABBING); DocIndice.Add(Chunk.TABBING);
            DocIndice.Add(Chunk.TABBING);
            DocIndice.Add(datosIndice.graficos.ToString());

            DocIndice.Add(Chunk.NEWLINE); DocIndice.Add(Chunk.NEWLINE);
            DocIndice.Add(Chunk.TABBING);
            DocIndice.Add("Identificación oficial");
            DocIndice.Add(Chunk.TABBING); DocIndice.Add(Chunk.TABBING); DocIndice.Add(Chunk.TABBING); DocIndice.Add(Chunk.TABBING); DocIndice.Add(Chunk.TABBING); DocIndice.Add(Chunk.TABBING); DocIndice.Add(Chunk.TABBING);
            DocIndice.Add(datosIndice.oficial.ToString());
            DocIndice.Add(Chunk.TABBING);
            DocIndice.Add(datosIndice.identificacion);

            DocIndice.Add(Chunk.NEWLINE); DocIndice.Add(Chunk.NEWLINE);
            DocIndice.Add(Chunk.TABBING);
            DocIndice.Add("Otros");
            DocIndice.Add(Chunk.TABBING); DocIndice.Add(Chunk.TABBING); DocIndice.Add(Chunk.TABBING); DocIndice.Add(Chunk.TABBING); DocIndice.Add(Chunk.TABBING); DocIndice.Add(Chunk.TABBING); DocIndice.Add(Chunk.TABBING);
            DocIndice.Add(Chunk.TABBING); DocIndice.Add(Chunk.TABBING);
            DocIndice.Add(datosIndice.otros_cantidad.ToString());
            DocIndice.Add(Chunk.NEWLINE);
            DocIndice.Add(Chunk.TABBING);
            DocIndice.Add("Otos Documentos");
            DocIndice.Add(Chunk.TABBING);
            DocIndice.Add(datosIndice.otros_texto);

            DocIndice.Font = FontFactory.GetFont("Arial", 10, Font.BOLD);
            DocIndice.Add(Chunk.NEWLINE);
            DocIndice.Add("_________________________________________________________________________________________________");
            DocIndice.Add(Chunk.NEWLINE); DocIndice.Add(Chunk.NEWLINE);
            DocIndice.Add(Chunk.TABBING);
            DocIndice.Add("Total");
            DocIndice.Add(Chunk.TABBING); DocIndice.Add(Chunk.TABBING); DocIndice.Add(Chunk.TABBING); DocIndice.Add(Chunk.TABBING); DocIndice.Add(Chunk.TABBING); DocIndice.Add(Chunk.TABBING); DocIndice.Add(Chunk.TABBING);
            DocIndice.Add(Chunk.TABBING); DocIndice.Add(Chunk.TABBING);
            DocIndice.Add((datosIndice.nreporte + datosIndice.datosgenerales + datosIndice.carta + datosIndice.personales + datosIndice.entrevista + datosIndice.preguntas + datosIndice.analisis + datosIndice.graficos + datosIndice.oficial + datosIndice.otros_cantidad).ToString());

            DocIndice.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            DocIndice.Add(Chunk.NEWLINE); DocIndice.Add(Chunk.NEWLINE); DocIndice.Add(Chunk.NEWLINE); DocIndice.Add(Chunk.NEWLINE);

            DocIndice.Add(Chunk.TABBING);
            DocIndice.Add("Integró: ");
            DocIndice.Add(Chunk.TABBING);
            DocIndice.Add(datosIndice.clave_ap);
            DocIndice.Add(Chunk.TABBING); DocIndice.Add(Chunk.TABBING); DocIndice.Add(Chunk.TABBING); DocIndice.Add(Chunk.TABBING); DocIndice.Add(Chunk.TABBING); DocIndice.Add(Chunk.TABBING); DocIndice.Add(Chunk.TABBING); DocIndice.Add(Chunk.TABBING);
            DocIndice.Add("Firma");

            docIndRep.Add(DocIndice);
            #endregion

            docIndRep.Close();

            byte[] bytesStream = ms.ToArray();

            ms = new MemoryStream();
            ms.Write(bytesStream, 0, bytesStream.Length);

            ms.Position = 0;

            return new FileStreamResult(ms, "application/pdf");

        }

        public ActionResult printRepAntecedentes(int sIdH) //Cat_controlador_Acciones XX
        {
            var datosRepAnt = RepAnt.getRepAntPrimero(sIdH).FirstOrDefault();
            var datosRepAnt_2 = RepAnt.getRepAntSegundo(sIdH).ToList();
            var datosRepAnt_3 = RepAnt.getRepAntTercero(sIdH).ToList();
            var datosRepAnt_4 = RepAnt.getRepAntCuarto(sIdH).FirstOrDefault();
            var datosRepAnt_5 = RepAnt.getRepAntQuinto(sIdH).ToList();

            MemoryStream msAnt = new MemoryStream();
            Document docRepAnt = new Document(PageSize.LETTER, 30f, 20f, 50f, 40f);
            PdfWriter pwRepAnt = PdfWriter.GetInstance(docRepAnt, msAnt);

            docRepAnt.Open();

            Chunk chunkAnt = new Chunk();

            #region Titulo Reporte
            Paragraph tituloReporte = new Paragraph();
            tituloReporte.Alignment = Element.ALIGN_CENTER;
            tituloReporte.Font = FontFactory.GetFont("Arial", 10, Font.BOLDITALIC);
            tituloReporte.Add("Concentrado Reporte de Antecedentes");
            tituloReporte.Add(Chunk.NEWLINE);
            docRepAnt.Add(tituloReporte);
            #endregion

            #region Principales
            Paragraph principales = new Paragraph();
            principales.Alignment = Element.ALIGN_JUSTIFIED;
            principales.Add(Chunk.NEWLINE);
            principales.Font = FontFactory.GetFont("Arial", 9, Font.NORMAL);
            principales.Add("CUIP");
            principales.Add(Chunk.TABBING);
            principales.Font = FontFactory.GetFont("Arial", 9, Font.BOLD);
            principales.Add(datosRepAnt.cuip);
            principales.Add(Chunk.TABBING); principales.Add(Chunk.TABBING);
            principales.Font = FontFactory.GetFont("Arial", 9, Font.NORMAL);
            principales.Add("Personal a Cargo:");
            principales.Add(Chunk.TABBING);
            principales.Font = FontFactory.GetFont("Arial", 9, Font.BOLD);
            principales.Add(datosRepAnt.personaCargo);
            principales.Add(Chunk.TABBING); principales.Add(Chunk.TABBING);
            principales.Font = FontFactory.GetFont("Arial", 9, Font.NORMAL);
            principales.Add("Cuantos:");
            principales.Add(Chunk.TABBING);
            principales.Font = FontFactory.GetFont("Arial", 9, Font.BOLD);
            principales.Add(datosRepAnt.cantidadPersonal);
            principales.Add(Chunk.NEWLINE);
            docRepAnt.Add(principales);
            #endregion

            #region Titulo Antecedente registral
            PdfPTable tableTituloRegistral = new PdfPTable(1);
            tableTituloRegistral.TotalWidth = 560f;
            tableTituloRegistral.LockedWidth = true;

            float[] widthsEstudio = new float[] { 1f };
            tableTituloRegistral.SetWidths(widthsEstudio);
            tableTituloRegistral.HorizontalAlignment = 0;
            tableTituloRegistral.SpacingBefore = 10f;
            tableTituloRegistral.SpacingAfter = 10f;

            PdfPCell cellTituloTituloRegistral = new PdfPCell(new Phrase("ANTECEDENTES REGISTRALES", new Font(Font.FontFamily.HELVETICA, 10f, Font.BOLDITALIC)));
            cellTituloTituloRegistral.HorizontalAlignment = 1;
            cellTituloTituloRegistral.BackgroundColor = new iTextSharp.text.BaseColor(234, 236, 238);
            cellTituloTituloRegistral.BorderColor = new iTextSharp.text.BaseColor(0, 0, 0);
            cellTituloTituloRegistral.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
            tableTituloRegistral.AddCell(cellTituloTituloRegistral);

            docRepAnt.Add(tableTituloRegistral);
            #endregion

            #region Datos Antecedentes Registrales
            Paragraph datosAntReg = new Paragraph();
            //int antreg = datosRepAnt_2.Count();
            foreach (var item in datosRepAnt_2)
            {
                datosAntReg.Alignment = Element.ALIGN_JUSTIFIED;
                datosAntReg.Font = FontFactory.GetFont("Arial", 9, Font.BOLD);
                datosAntReg.Add("Tipo Registro");
                datosAntReg.Add(Chunk.TABBING);
                datosAntReg.Font = FontFactory.GetFont("Arial", 9, Font.NORMAL);
                datosAntReg.Add(item.tipoRegistro);
                datosAntReg.Add(Chunk.TABBING); datosAntReg.Add(Chunk.TABBING);
                datosAntReg.Font = FontFactory.GetFont("Arial", 9, Font.BOLD);
                datosAntReg.Add("N.C.P.");
                datosAntReg.Add(Chunk.TABBING);
                datosAntReg.Font = FontFactory.GetFont("Arial", 9, Font.NORMAL);
                datosAntReg.Add(item.ncp);
                datosAntReg.Add(Chunk.NEWLINE);
                datosAntReg.Font = FontFactory.GetFont("Arial", 9, Font.BOLD);
                datosAntReg.Add("Procedencia");
                datosAntReg.Add(Chunk.TABBING);
                datosAntReg.Font = FontFactory.GetFont("Arial", 9, Font.NORMAL);
                datosAntReg.Add(item.procedencia);
                datosAntReg.Add(Chunk.NEWLINE);
            }
            docRepAnt.Add(datosAntReg);
            #endregion

            #region observacione registral
            Paragraph observacionRegistral = new Paragraph();
            observacionRegistral.Alignment = Element.ALIGN_JUSTIFIED;
            observacionRegistral.Font = FontFactory.GetFont("Arial", 9, Font.BOLD);
            observacionRegistral.Add("Observaciones:");
            observacionRegistral.Add(Chunk.TABBING);
            observacionRegistral.Font = FontFactory.GetFont("Arial", 9, Font.NORMAL);
            observacionRegistral.Add(datosRepAnt.observacionRegistral);
            observacionRegistral.Add(Chunk.NEWLINE);
            docRepAnt.Add(observacionRegistral);
            #endregion

            #region Titulo Plataforma Mexico SUIC
            PdfPTable tableTituloSUIC = new PdfPTable(1);
            tableTituloSUIC.TotalWidth = 560f;
            tableTituloSUIC.LockedWidth = true;

            float[] widthSUIC = new float[] { 1f };
            tableTituloSUIC.SetWidths(widthSUIC);
            tableTituloSUIC.HorizontalAlignment = 0;
            tableTituloSUIC.SpacingBefore = 10f;
            tableTituloSUIC.SpacingAfter = 10f;

            PdfPCell cellTituloTituloSuic = new PdfPCell(new Phrase("PLATAFORMA MEXICO (SUIC)", new Font(Font.FontFamily.HELVETICA, 10f, Font.BOLDITALIC)));
            cellTituloTituloSuic.HorizontalAlignment = 1;
            cellTituloTituloSuic.BackgroundColor = new iTextSharp.text.BaseColor(234, 236, 238);
            cellTituloTituloSuic.BorderColor = new iTextSharp.text.BaseColor(0, 0, 0);
            cellTituloTituloSuic.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
            tableTituloSUIC.AddCell(cellTituloTituloSuic);

            docRepAnt.Add(tableTituloSUIC);
            #endregion

            #region Datos SUIC
            Paragraph datoSuic = new Paragraph();
            datoSuic.Alignment = Element.ALIGN_JUSTIFIED;
            datoSuic.Font = FontFactory.GetFont("Arial", 9, Font.NORMAL);
            datoSuic.Add(datosRepAnt.cSuic);
            datoSuic.Add(Chunk.NEWLINE);
            docRepAnt.Add(datoSuic);
            #endregion

            #region Titulo RNPSP
            PdfPTable tableTituloRNPSP = new PdfPTable(1);
            tableTituloRNPSP.TotalWidth = 560f;
            tableTituloRNPSP.LockedWidth = true;

            float[] widthRnpsp = new float[] { 1f };
            tableTituloRNPSP.SetWidths(widthRnpsp);
            tableTituloRNPSP.HorizontalAlignment = 0;
            tableTituloRNPSP.SpacingBefore = 10f;
            tableTituloRNPSP.SpacingAfter = 10f;

            PdfPCell cellTituloTituloRnpsp = new PdfPCell(new Phrase("RNPSP", new Font(Font.FontFamily.HELVETICA, 10f, Font.BOLDITALIC)));
            cellTituloTituloRnpsp.HorizontalAlignment = 1;
            cellTituloTituloRnpsp.BackgroundColor = new iTextSharp.text.BaseColor(234, 236, 238);
            cellTituloTituloRnpsp.BorderColor = new iTextSharp.text.BaseColor(0, 0, 0);
            cellTituloTituloRnpsp.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
            tableTituloRNPSP.AddCell(cellTituloTituloRnpsp);

            docRepAnt.Add(tableTituloRNPSP);
            #endregion

            #region Datos RNPSP
            Paragraph datoRnpsp = new Paragraph();
            datoRnpsp.Alignment = Element.ALIGN_JUSTIFIED;
            datoRnpsp.Font = FontFactory.GetFont("Arial", 9, Font.NORMAL);
            datoRnpsp.Add(datosRepAnt.cRnpsp);
            datoRnpsp.Add(Chunk.NEWLINE);
            docRepAnt.Add(datoRnpsp);
            #endregion

            #region Titulo TELESCAN
            PdfPTable tableTituloTelescan = new PdfPTable(1);
            tableTituloTelescan.TotalWidth = 560f;
            tableTituloTelescan.LockedWidth = true;

            float[] widthTelescan = new float[] { 1f };
            tableTituloTelescan.SetWidths(widthTelescan);
            tableTituloTelescan.HorizontalAlignment = 0;
            tableTituloTelescan.SpacingBefore = 10f;
            tableTituloTelescan.SpacingAfter = 10f;

            PdfPCell cellTituloTituloTelescan = new PdfPCell(new Phrase("TELESCAN", new Font(Font.FontFamily.HELVETICA, 10f, Font.BOLDITALIC)));
            cellTituloTituloTelescan.HorizontalAlignment = 1;
            cellTituloTituloTelescan.BackgroundColor = new iTextSharp.text.BaseColor(234, 236, 238);
            cellTituloTituloTelescan.BorderColor = new iTextSharp.text.BaseColor(0, 0, 0);
            cellTituloTituloTelescan.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
            tableTituloTelescan.AddCell(cellTituloTituloTelescan);

            docRepAnt.Add(tableTituloTelescan);
            #endregion

            #region Datos TELESCAN
            Paragraph datoTelescan = new Paragraph();
            datoTelescan.Alignment = Element.ALIGN_JUSTIFIED;
            datoTelescan.Font = FontFactory.GetFont("Arial", 9, Font.NORMAL);
            datoTelescan.Add(datosRepAnt.cTelscan);
            datoTelescan.Add(Chunk.NEWLINE);
            docRepAnt.Add(datoTelescan);
            #endregion

            #region Titulo Antecedente Penal
            PdfPTable tableTituloPenal = new PdfPTable(1);
            tableTituloPenal.TotalWidth = 560f;
            tableTituloPenal.LockedWidth = true;

            float[] widthsPenal = new float[] { 1f };
            tableTituloPenal.SetWidths(widthsPenal);
            tableTituloPenal.HorizontalAlignment = 0;
            tableTituloPenal.SpacingBefore = 10f;
            tableTituloPenal.SpacingAfter = 10f;

            PdfPCell cellTituloTituloPenal = new PdfPCell(new Phrase("ANTECEDENTES PENALES", new Font(Font.FontFamily.HELVETICA, 10f, Font.BOLDITALIC)));
            cellTituloTituloPenal.HorizontalAlignment = 1;
            cellTituloTituloPenal.BackgroundColor = new iTextSharp.text.BaseColor(234, 236, 238);
            cellTituloTituloPenal.BorderColor = new iTextSharp.text.BaseColor(0, 0, 0);
            cellTituloTituloPenal.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
            tableTituloPenal.AddCell(cellTituloTituloPenal);

            docRepAnt.Add(tableTituloPenal);
            #endregion

            #region Datos Antecedentes Penales
            PdfPTable tablaPenal = new PdfPTable(2);
            tablaPenal.TotalWidth = 560;
            tablaPenal.LockedWidth = true;

            float[] widths = new float[] { 1f, 3f };
            tablaPenal.SetWidths(widths);
            tablaPenal.HorizontalAlignment = 0;
            tablaPenal.SpacingAfter = 5f;
            tablaPenal.SpacingBefore = 10f;
            tablaPenal.DefaultCell.Border = 1;

            foreach (var itemPen in datosRepAnt_3)
            {
                PdfPCell cel5a = new PdfPCell(new Phrase("", new Font(Font.FontFamily.HELVETICA, 9F, Font.NORMAL)));
                cel5a.HorizontalAlignment = Element.ALIGN_LEFT;
                cel5a.Border = 0;

                PdfPCell cel5b = new PdfPCell(new Phrase("Constancia de NO Antecedentes Penales de la Evaluación " + itemPen.idhistorico + " del " + itemPen.fechaEvalPenal, new Font(Font.FontFamily.HELVETICA, 9f, Font.BOLD)));
                cel5b.HorizontalAlignment = Element.ALIGN_LEFT;
                cel5b.Border = 0;

                PdfPCell cel4a = new PdfPCell(new Phrase("Lugar - Fecha Exp:", new Font(Font.FontFamily.HELVETICA, 9F, Font.BOLD)));
                cel4a.HorizontalAlignment = Element.ALIGN_LEFT;
                cel4a.Border = 0;

                PdfPCell cel4b = new PdfPCell(new Phrase(itemPen.lugarFechaPenal, new Font(Font.FontFamily.HELVETICA, 9f, Font.NORMAL)));
                cel4b.HorizontalAlignment = Element.ALIGN_LEFT;
                cel4b.Border = 0;

                PdfPCell cel2a = new PdfPCell(new Phrase("Num. Constancia:", new Font(Font.FontFamily.HELVETICA, 9f, Font.BOLD)));
                cel2a.HorizontalAlignment = Element.ALIGN_LEFT;
                cel2a.Border = 0;

                PdfPCell cel2b = new PdfPCell(new Phrase(itemPen.constanciaPenal, new Font(Font.FontFamily.HELVETICA, 9f, Font.NORMAL)));
                cel2b.HorizontalAlignment = Element.ALIGN_LEFT;
                cel2b.Border = 0;

                PdfPCell cel1a = new PdfPCell(new Phrase("Observaciones:", new Font(Font.FontFamily.HELVETICA, 9f, Font.BOLD)));
                cel1a.HorizontalAlignment = Element.ALIGN_LEFT;
                cel1a.Border = 0;

                PdfPCell cel1b = new PdfPCell(new Phrase(itemPen.obsPenal, new Font(Font.FontFamily.HELVETICA, 9f, Font.NORMAL)));
                cel1b.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                cel1b.Border = 0;

                PdfPCell cel3a = new PdfPCell(new Phrase("\n", new Font(Font.FontFamily.HELVETICA, 9f, Font.NORMAL)));
                cel3a.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                cel3a.Border = 0;

                PdfPCell cel3b = new PdfPCell(new Phrase("\n", new Font(Font.FontFamily.HELVETICA, 9f, Font.NORMAL)));
                cel3b.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                cel3b.Border = 0;

                tablaPenal.AddCell(cel5a);
                tablaPenal.AddCell(cel5b);

                tablaPenal.AddCell(cel2a);
                tablaPenal.AddCell(cel2b);

                tablaPenal.AddCell(cel4a);
                tablaPenal.AddCell(cel4b);

                tablaPenal.AddCell(cel1a);
                tablaPenal.AddCell(cel1b);

                tablaPenal.AddCell(cel3a);
                tablaPenal.AddCell(cel3b);
            }

            docRepAnt.Add(tablaPenal);
            #endregion

            #region Titulo Antecedente Admivos
            PdfPTable tableTituloAdministrativo = new PdfPTable(1);
            tableTituloAdministrativo.TotalWidth = 560f;
            tableTituloAdministrativo.LockedWidth = true;

            float[] widthsAdmivo = new float[] { 1f };
            tableTituloAdministrativo.SetWidths(widthsAdmivo);
            tableTituloAdministrativo.HorizontalAlignment = 0;
            tableTituloAdministrativo.SpacingBefore = 10f;
            tableTituloAdministrativo.SpacingAfter = 10f;

            PdfPCell cellTituloTituloAdmivo = new PdfPCell(new Phrase("ANTECEDENTES ADMINISTRATIVOS", new Font(Font.FontFamily.HELVETICA, 10f, Font.BOLDITALIC)));
            cellTituloTituloAdmivo.HorizontalAlignment = 1;
            cellTituloTituloAdmivo.BackgroundColor = new iTextSharp.text.BaseColor(234, 236, 238);
            cellTituloTituloAdmivo.BorderColor = new iTextSharp.text.BaseColor(0, 0, 0);
            cellTituloTituloAdmivo.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
            tableTituloAdministrativo.AddCell(cellTituloTituloAdmivo);

            docRepAnt.Add(tableTituloAdministrativo);
            #endregion

            #region Titulos Antecedentes administrativos
            Paragraph tituloantAdm = new Paragraph();
            tituloantAdm.Alignment = Element.ALIGN_CENTER;
            //tituloantAdm.Add(Chunk.NEWLINE);
            //tituloantAdm.Font = FontFactory.GetFont("Arial", 9, Font.BOLDITALIC);
            //tituloantAdm.Add("ANTECEDENTES ADMINISTRATIVOS");
            tituloantAdm.Add(Chunk.NEWLINE);
            tituloantAdm.Font = FontFactory.GetFont("Arial", 9, Font.NORMAL);
            tituloantAdm.Add("Resultado de Inhabilitacion");
            tituloantAdm.Add(Chunk.NEWLINE);
            docRepAnt.Add(tituloantAdm);
            #endregion

            #region Antecedentes administrativos
            Paragraph antAdm = new Paragraph();
            antAdm.Alignment = Element.ALIGN_JUSTIFIED;
            antAdm.Font = FontFactory.GetFont("Arial", 9, Font.NORMAL);
            antAdm.Add("Estatal");
            antAdm.Add(Chunk.TABBING);
            antAdm.Font = FontFactory.GetFont("Arial", 9, Font.BOLD);
            antAdm.Add(datosRepAnt.inhabilitacionEstatal);
            antAdm.Font = FontFactory.GetFont("Arial", 9, Font.NORMAL);
            antAdm.Add(Chunk.TABBING); antAdm.Add(Chunk.TABBING);
            antAdm.Add("No. Constancia");
            antAdm.Add(Chunk.TABBING); antAdm.Add(Chunk.TABBING);
            antAdm.Font = FontFactory.GetFont("Arial", 9, Font.BOLD);
            antAdm.Add(datosRepAnt.constanciaInhabilitacionEstatal);
            antAdm.Add(Chunk.TABBING); antAdm.Add(Chunk.TABBING); antAdm.Add(Chunk.TABBING);
            antAdm.Font = FontFactory.GetFont("Arial", 9, Font.NORMAL);
            antAdm.Add("Fecha");
            antAdm.Add(Chunk.TABBING);
            antAdm.Font = FontFactory.GetFont("Arial", 9, Font.BOLD);
            antAdm.Add(datosRepAnt.fechaConstanciaEstatal);
            antAdm.Add(Chunk.NEWLINE);

            docRepAnt.Add(antAdm);
            #endregion

            #region resultado antecedentes federal
            Paragraph tituloantFed = new Paragraph();
            tituloantFed.Alignment = Element.ALIGN_CENTER;
            tituloantFed.Add(Chunk.NEWLINE);
            tituloantFed.Font = FontFactory.GetFont("Arial", 9, Font.NORMAL);
            tituloantFed.Add("Resultado de Antecedente");
            tituloantFed.Add(Chunk.NEWLINE);
            docRepAnt.Add(tituloantFed);

            #endregion

            #region datos antecedente federal
            Paragraph antAdmFed = new Paragraph();
            antAdmFed.Alignment = Element.ALIGN_JUSTIFIED;
            antAdmFed.Font = FontFactory.GetFont("Arial", 9, Font.NORMAL);
            antAdmFed.Add("Federal");
            antAdmFed.Add(Chunk.TABBING);
            antAdmFed.Font = FontFactory.GetFont("Arial", 9, Font.BOLD);
            antAdmFed.Add(datosRepAnt.antecedenteFederal);
            antAdmFed.Font = FontFactory.GetFont("Arial", 9, Font.NORMAL);
            antAdmFed.Add(Chunk.TABBING); antAdmFed.Add(Chunk.TABBING);
            antAdmFed.Add("No. Constancia");
            antAdmFed.Add(Chunk.TABBING); antAdmFed.Add(Chunk.TABBING);
            antAdmFed.Font = FontFactory.GetFont("Arial", 9, Font.BOLD);
            antAdmFed.Add(datosRepAnt.constanciaAntecedenteFederal);
            antAdmFed.Add(Chunk.TABBING); antAdmFed.Add(Chunk.TABBING); antAdmFed.Add(Chunk.TABBING);
            antAdmFed.Font = FontFactory.GetFont("Arial", 9, Font.NORMAL);
            antAdmFed.Add("Fecha");
            antAdmFed.Add(Chunk.TABBING);
            antAdmFed.Font = FontFactory.GetFont("Arial", 9, Font.BOLD);
            antAdmFed.Add(datosRepAnt.fechaAntecedenteFederal);
            antAdmFed.Add(Chunk.NEWLINE);
            antAdmFed.Font = FontFactory.GetFont("Arial", 9, Font.BOLD);
            antAdmFed.Add("Obsevación:");
            antAdmFed.Add(Chunk.TABBING);
            antAdmFed.Font = FontFactory.GetFont("Arial", 9, Font.NORMAL);
            antAdmFed.Add(datosRepAnt.observacionAdministrativo);
            antAdmFed.Add(Chunk.NEWLINE);

            docRepAnt.Add(antAdmFed);
            #endregion

            if (datosRepAnt_4 != null)
            {
                #region Titulo Aplicativo
                Paragraph tituloAplicativo = new Paragraph();
                tituloAplicativo.Add(Chunk.NEWLINE);
                tituloAplicativo.Alignment = Element.ALIGN_CENTER;
                tituloAplicativo.Font = FontFactory.GetFont("Arial", 9, Font.BOLDITALIC);
                tituloAplicativo.Add("REGISTRO NACIONAL DE PERSONAL DE SEGURIDAD PUBLICA");
                tituloAplicativo.Add(Chunk.NEWLINE);
                docRepAnt.Add(tituloAplicativo);
                #endregion

                #region Titulo Aplicativ
                PdfPTable tableTituloAplica = new PdfPTable(1);
                tableTituloAplica.TotalWidth = 560f;
                tableTituloAplica.LockedWidth = true;

                float[] widthsAplicativo = new float[] { 1f };
                tableTituloAplica.SetWidths(widthsAplicativo);
                tableTituloAplica.HorizontalAlignment = 0;
                tableTituloAplica.SpacingBefore = 10f;
                tableTituloAplica.SpacingAfter = 10f;

                PdfPCell cellTituloTituloAplicativo = new PdfPCell(new Phrase("REGISTRO NACIONAL DE PERSONAL DE SEGURIDAD PUBLICA", new Font(Font.FontFamily.HELVETICA, 10f, Font.BOLDITALIC)));
                cellTituloTituloAplicativo.HorizontalAlignment = 1;
                cellTituloTituloAplicativo.BackgroundColor = new iTextSharp.text.BaseColor(234, 236, 238);
                cellTituloTituloAplicativo.BorderColor = new iTextSharp.text.BaseColor(0, 0, 0);
                cellTituloTituloAplicativo.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                tableTituloAplica.AddCell(cellTituloTituloAplicativo);

                docRepAnt.Add(tableTituloAplica);
                #endregion

                #region Dato Aplicativo
                Paragraph datoAplicativo = new Paragraph();
                datoAplicativo.Alignment = Element.ALIGN_LEFT;
                datoAplicativo.Font = FontFactory.GetFont("Arial", 9, Font.BOLD);
                if (datosRepAnt_4.aplicativoActual == true)
                    datoAplicativo.Add("Adscripcion Actual");
                else
                    datoAplicativo.Add("Adscripcion Anterior");
                datoAplicativo.Add(Chunk.NEWLINE);
                datoAplicativo.Font = FontFactory.GetFont("Arial", 9, Font.NORMAL);
                datoAplicativo.Add("Observaciones:");
                datoAplicativo.Add(Chunk.TABBING);
                datoAplicativo.Add(datosRepAnt_4.observaAplicativo);
                datoAplicativo.Add(Chunk.NEWLINE);
                docRepAnt.Add(datoAplicativo);
                #endregion
            }

            #region Titulo de Control Vehicular TABLE
            PdfPTable tableCtrVeh = new PdfPTable(1);
            tableCtrVeh.TotalWidth = 560f;
            tableCtrVeh.LockedWidth = true;

            float[] widthsVeh = new float[] { 1f };
            tableCtrVeh.SetWidths(widthsVeh);
            tableCtrVeh.HorizontalAlignment = 0;
            tableCtrVeh.SpacingBefore = 10f;
            tableCtrVeh.SpacingAfter = 10f;

            PdfPCell cellTituloTituloVeh = new PdfPCell(new Phrase("REPORTE DE CONTROL VEHICULAR", new Font(Font.FontFamily.HELVETICA, 10f, Font.BOLDITALIC)));
            cellTituloTituloVeh.HorizontalAlignment = 1;
            cellTituloTituloVeh.BackgroundColor = new iTextSharp.text.BaseColor(234, 236, 238);
            cellTituloTituloVeh.BorderColor = new iTextSharp.text.BaseColor(0, 0, 0);
            cellTituloTituloVeh.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
            tableCtrVeh.AddCell(cellTituloTituloVeh);

            docRepAnt.Add(tableCtrVeh);
            #endregion

            #region Dato Vehicular oficios
            Paragraph datoVehicular = new Paragraph();
            foreach (var itemV in datosRepAnt_5)
            {
                datoVehicular.Alignment = Element.ALIGN_JUSTIFIED;
                datoVehicular.Font = FontFactory.GetFont("Arial", 9, Font.BOLD);
                datoVehicular.Add("Num. Oficio Enviado");
                datoVehicular.Font = FontFactory.GetFont("Arial", 9, Font.NORMAL);
                datoVehicular.Add(Chunk.TABBING);
                datoVehicular.Add(itemV.ofiEnvVeh);
                datoVehicular.Add(Chunk.TABBING); datoVehicular.Add(Chunk.TABBING); datoVehicular.Add(Chunk.TABBING); datoVehicular.Add(Chunk.TABBING);
                datoVehicular.Font = FontFactory.GetFont("Arial", 9, Font.BOLD);
                datoVehicular.Add("Fecha");
                datoVehicular.Font = FontFactory.GetFont("Arial", 9, Font.NORMAL);
                datoVehicular.Add(Chunk.TABBING);
                datoVehicular.Add(itemV.fenviado);
                datoVehicular.Add(Chunk.NEWLINE);
                datoVehicular.Font = FontFactory.GetFont("Arial", 9, Font.BOLD);
                datoVehicular.Add("Num. Oficio Recibido");
                datoVehicular.Font = FontFactory.GetFont("Arial", 9, Font.NORMAL);
                datoVehicular.Add(Chunk.TABBING);
                datoVehicular.Add(itemV.ofiRecVeh);
                datoVehicular.Add(Chunk.TABBING); datoVehicular.Add(Chunk.TABBING); datoVehicular.Add(Chunk.TABBING);
                datoVehicular.Font = FontFactory.GetFont("Arial", 9, Font.BOLD);
                datoVehicular.Add("Fecha");
                datoVehicular.Font = FontFactory.GetFont("Arial", 9, Font.NORMAL);
                datoVehicular.Add(Chunk.TABBING);
                datoVehicular.Add(itemV.frecibido);
                datoVehicular.Add(Chunk.NEWLINE);
                if (itemV.vehiculoencontrado == false)
                {
                    datoVehicular.Font = FontFactory.GetFont("Arial", 9, Font.BOLD);
                    datoVehicular.Add("Observaciones");
                    datoVehicular.Font = FontFactory.GetFont("Arial", 9, Font.NORMAL);
                    datoVehicular.Add(Chunk.TABBING);
                    datoVehicular.Add(itemV.observaVehiculo);
                    datoVehicular.Add(Chunk.NEWLINE);
                }
                else
                {
                    datoVehicular.Alignment = Element.ALIGN_JUSTIFIED;
                    datoVehicular.Font = FontFactory.GetFont("Arial", 9, Font.BOLDITALIC);
                    datoVehicular.Add("RESULTADOS");
                    datoVehicular.Add(Chunk.NEWLINE);
                    datoVehicular.Font = FontFactory.GetFont("Arial", 9, Font.BOLD);
                    datoVehicular.Add("Tipo");
                    datoVehicular.Add(Chunk.TABBING);
                    datoVehicular.Font = FontFactory.GetFont("Arial", 9, Font.NORMAL);
                    datoVehicular.Add(itemV.tipo);
                    datoVehicular.Add(Chunk.TABBING); datoVehicular.Add(Chunk.TABBING);
                    datoVehicular.Font = FontFactory.GetFont("Arial", 9, Font.BOLD);
                    datoVehicular.Add("Placa");
                    datoVehicular.Add(Chunk.TABBING);
                    datoVehicular.Font = FontFactory.GetFont("Arial", 9, Font.NORMAL);
                    datoVehicular.Add(itemV.placa);
                    datoVehicular.Add(Chunk.TABBING); datoVehicular.Add(Chunk.TABBING);
                    datoVehicular.Font = FontFactory.GetFont("Arial", 9, Font.BOLD);
                    datoVehicular.Add("Serie");
                    datoVehicular.Add(Chunk.TABBING);
                    datoVehicular.Font = FontFactory.GetFont("Arial", 9, Font.NORMAL);
                    datoVehicular.Add(itemV.serie);
                    datoVehicular.Add(Chunk.NEWLINE);
                    datoVehicular.Font = FontFactory.GetFont("Arial", 9, Font.BOLD);
                    datoVehicular.Add("Marca");
                    datoVehicular.Add(Chunk.TABBING);
                    datoVehicular.Font = FontFactory.GetFont("Arial", 9, Font.NORMAL);
                    datoVehicular.Add(itemV.marca);
                    datoVehicular.Add(Chunk.TABBING); datoVehicular.Add(Chunk.TABBING);
                    datoVehicular.Font = FontFactory.GetFont("Arial", 9, Font.BOLD);
                    datoVehicular.Add("Clase");
                    datoVehicular.Add(Chunk.TABBING);
                    datoVehicular.Font = FontFactory.GetFont("Arial", 9, Font.NORMAL);
                    datoVehicular.Add(itemV.clase);
                    datoVehicular.Add(Chunk.TABBING); datoVehicular.Add(Chunk.TABBING);
                    datoVehicular.Font = FontFactory.GetFont("Arial", 9, Font.BOLD);
                    datoVehicular.Add("Modelo");
                    datoVehicular.Add(Chunk.TABBING);
                    datoVehicular.Font = FontFactory.GetFont("Arial", 9, Font.NORMAL);
                    datoVehicular.Add(itemV.modelo);
                    datoVehicular.Add(Chunk.NEWLINE);

                    datoVehicular.Font = FontFactory.GetFont("Arial", 9, Font.BOLD);
                    datoVehicular.Add("Fecha de Alta");
                    datoVehicular.Add(Chunk.TABBING);
                    datoVehicular.Font = FontFactory.GetFont("Arial", 9, Font.NORMAL);
                    datoVehicular.Add(itemV.faltaVehicular);
                    datoVehicular.Add(Chunk.NEWLINE); datoVehicular.Add(Chunk.NEWLINE);
                }
                docRepAnt.Add(datoVehicular);
            }
            #endregion

            #region Titulo Publicaciones y redes sociales
            PdfPTable tableTituloPublicaciones = new PdfPTable(1);
            tableTituloPublicaciones.TotalWidth = 560f;
            tableTituloPublicaciones.LockedWidth = true;

            float[] widthPublicacion = new float[] { 1f };
            tableTituloPublicaciones.SetWidths(widthPublicacion);
            tableTituloPublicaciones.HorizontalAlignment = 0;
            tableTituloPublicaciones.SpacingBefore = 10f;
            tableTituloPublicaciones.SpacingAfter = 10f;

            PdfPCell cellTituloTituloPublicacion = new PdfPCell(new Phrase("PUBLICACIONES Y REDES SOCIALES)", new Font(Font.FontFamily.HELVETICA, 10f, Font.BOLDITALIC)));
            cellTituloTituloPublicacion.HorizontalAlignment = 1;
            cellTituloTituloPublicacion.BackgroundColor = new iTextSharp.text.BaseColor(234, 236, 238);
            cellTituloTituloPublicacion.BorderColor = new iTextSharp.text.BaseColor(0, 0, 0);
            cellTituloTituloPublicacion.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
            tableTituloPublicaciones.AddCell(cellTituloTituloPublicacion);

            docRepAnt.Add(tableTituloPublicaciones);
            #endregion

            #region Datos Publicacion y redes sociales
            Paragraph datoPublicacion = new Paragraph();
            datoPublicacion.Alignment = Element.ALIGN_JUSTIFIED;
            datoPublicacion.Font = FontFactory.GetFont("Arial", 9, Font.NORMAL);
            datoPublicacion.Add(datosRepAnt.publicacion);
            datoPublicacion.Add(Chunk.NEWLINE);
            docRepAnt.Add(datoPublicacion);
            #endregion

            #region Titulo Riesgos detectados en evaluaciones anteriores
            PdfPTable tableTituloRiesgos = new PdfPTable(1);
            tableTituloRiesgos.TotalWidth = 560f;
            tableTituloRiesgos.LockedWidth = true;

            float[] widthRiesgos = new float[] { 1f };
            tableTituloRiesgos.SetWidths(widthRiesgos);
            tableTituloRiesgos.HorizontalAlignment = 0;
            tableTituloRiesgos.SpacingBefore = 10f;
            tableTituloRiesgos.SpacingAfter = 10f;

            PdfPCell cellTituloTituloRiesgos = new PdfPCell(new Phrase("RIESGOS DETECTADOS EN EVALUACIONES ANTERIORES)", new Font(Font.FontFamily.HELVETICA, 10f, Font.BOLDITALIC)));
            cellTituloTituloRiesgos.HorizontalAlignment = 1;
            cellTituloTituloRiesgos.BackgroundColor = new iTextSharp.text.BaseColor(234, 236, 238);
            cellTituloTituloRiesgos.BorderColor = new iTextSharp.text.BaseColor(0, 0, 0);
            cellTituloTituloRiesgos.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
            tableTituloRiesgos.AddCell(cellTituloTituloRiesgos);

            docRepAnt.Add(tableTituloRiesgos);
            #endregion

            #region Datos Riesgos detectados en evaluaciones anteriores
            Paragraph datoRiesgos = new Paragraph();
            datoRiesgos.Alignment = Element.ALIGN_JUSTIFIED;
            datoRiesgos.Font = FontFactory.GetFont("Arial", 9, Font.NORMAL);
            datoRiesgos.Add(datosRepAnt.cEvaluacionAnterior);
            datoRiesgos.Add(Chunk.NEWLINE);
            docRepAnt.Add(datoRiesgos);
            #endregion

            #region Titulo y Datos sociodemograficos
            PdfPTable tableTituloContexto = new PdfPTable(1);
            tableTituloContexto.TotalWidth = 560f;
            tableTituloContexto.LockedWidth = true;

            float[] widthscontexto = new float[] { 1f };
            tableTituloContexto.SetWidths(widthscontexto);
            tableTituloContexto.HorizontalAlignment = 0;
            tableTituloContexto.SpacingBefore = 10f;
            tableTituloContexto.SpacingAfter = 10f;

            PdfPCell cellTituloTituloContexto = new PdfPCell(new Phrase("DATOS SOCIODEMOGRAFICOS", new Font(Font.FontFamily.HELVETICA, 10f, Font.BOLDITALIC)));
            cellTituloTituloContexto.HorizontalAlignment = 1;
            cellTituloTituloContexto.BackgroundColor = new iTextSharp.text.BaseColor(234, 236, 238);
            cellTituloTituloContexto.BorderColor = new iTextSharp.text.BaseColor(0, 0, 0);
            cellTituloTituloContexto.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
            tableTituloContexto.AddCell(cellTituloTituloContexto);

            docRepAnt.Add(tableTituloContexto);
            #endregion

            #region Dato Contexto
            Paragraph datoContexto = new Paragraph();
            datoContexto.Alignment = Element.ALIGN_JUSTIFIED;
            datoContexto.Font = FontFactory.GetFont("Arial", 9, Font.NORMAL);
            datoContexto.Add(datosRepAnt.contexto);
            datoContexto.Add(Chunk.NEWLINE);
            docRepAnt.Add(datoContexto);
            #endregion

            #region Titulo Analisis - Riesgo detectado
            PdfPTable tableTituloAnalisis = new PdfPTable(1);
            tableTituloAnalisis.TotalWidth = 560f;
            tableTituloAnalisis.LockedWidth = true;

            float[] widthAnalisis = new float[] { 1f };
            tableTituloAnalisis.SetWidths(widthAnalisis);
            tableTituloAnalisis.HorizontalAlignment = 0;
            tableTituloAnalisis.SpacingBefore = 10f;
            tableTituloAnalisis.SpacingAfter = 10f;

            PdfPCell cellTituloTituloAnalisis = new PdfPCell(new Phrase("ANALISIS DE INFORMACION)", new Font(Font.FontFamily.HELVETICA, 10f, Font.BOLDITALIC)));
            cellTituloTituloAnalisis.HorizontalAlignment = 1;
            cellTituloTituloAnalisis.BackgroundColor = new iTextSharp.text.BaseColor(234, 236, 238);
            cellTituloTituloAnalisis.BorderColor = new iTextSharp.text.BaseColor(0, 0, 0);
            cellTituloTituloAnalisis.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
            tableTituloAnalisis.AddCell(cellTituloTituloAnalisis);

            docRepAnt.Add(tableTituloAnalisis);
            #endregion

            #region Datos Analisis
            Paragraph datoAnalisis = new Paragraph();
            datoAnalisis.Alignment = Element.ALIGN_JUSTIFIED;
            datoAnalisis.Font = FontFactory.GetFont("Arial", 9, Font.NORMAL);
            datoAnalisis.Add(datosRepAnt.cAnalisis);
            datoAnalisis.Add(Chunk.NEWLINE);
            docRepAnt.Add(datoAnalisis);
            #endregion

            docRepAnt.Close();
            byte[] bytesStreamAnt = msAnt.ToArray();
            msAnt = new MemoryStream();
            msAnt.Write(bytesStreamAnt, 0, bytesStreamAnt.Length);
            msAnt.Position = 0;
            return new FileStreamResult(msAnt, "application/pdf");
        }

        public ActionResult printTestPoligrafista(int id, int opcion)
        {
            var datosEvaluacionPoligrafista = Test.getEvaluacionDesempenoImprimir(id, opcion).FirstOrDefault();

            MemoryStream ms = new MemoryStream();

            Document docEvaPol = new Document(PageSize.LETTER, 30f, 20f, 50f, 40f);
            PdfWriter pwEvalPol = PdfWriter.GetInstance(docEvaPol, ms);

            pwEvalPol.PageEvent = new HeaderFooterDesempenoPoligrafista();

            docEvaPol.Open();

            Chunk chunk = new Chunk();

            #region Fecha
            Paragraph poligrafista = new Paragraph();            
            poligrafista.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            poligrafista.Add("_________________________________________________________________________________________________");
            poligrafista.Add(Chunk.NEWLINE);
            poligrafista.Font = FontFactory.GetFont("Arial", 10, Font.BOLD);
            poligrafista.Add("DEPENDENCIA: CENTRO ESTATAL DE CONTROL DE CONFIANZA CERTIFICADO");
            poligrafista.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            poligrafista.Add(Chunk.TABBING);
            poligrafista.Add(Chunk.TABBING);
            poligrafista.Add(datosEvaluacionPoligrafista.fechaTest);
            poligrafista.Add(Chunk.NEWLINE); poligrafista.Add(Chunk.NEWLINE);

            docEvaPol.Add(poligrafista);
            #endregion

            #region Evaluado
            Paragraph evaluado = new Paragraph();
            evaluado.Alignment = Element.ALIGN_CENTER;
            evaluado.Font = FontFactory.GetFont("Arial", 10, Font.BOLD);
            evaluado.Add("Datos del Evaluado");
            evaluado.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            evaluado.Add(Chunk.NEWLINE);

            docEvaPol.Add(evaluado);
            Paragraph evaluado2 = new Paragraph();
            evaluado2.Alignment = Element.ALIGN_LEFT;
            evaluado2.Font = FontFactory.GetFont("Arial", 10, Font.BOLD);
            evaluado2.Add("Nombre completo: ");
            evaluado2.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            evaluado2.Add(Chunk.TABBING); evaluado2.Add(Chunk.TABBING);
            evaluado2.Add(datosEvaluacionPoligrafista.evaluado);
            evaluado2.Add(Chunk.NEWLINE);
            evaluado2.Font = FontFactory.GetFont("Arial", 10, Font.BOLD);
            evaluado2.Add("Cargo: ");
            evaluado2.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            evaluado2.Add("Poligrafista");

            docEvaPol.Add(evaluado2);

            #endregion

            #region datosEvaluacion
            Paragraph evalDes = new Paragraph();
            evalDes.Alignment = Element.ALIGN_JUSTIFIED;
            evalDes.Add(Chunk.NEWLINE);
            evalDes.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            evalDes.Add("Los factores a evaluar, van orientados, no sólo hacia la evaluación del desempeño laboral, sino que pretenden evaluar simúltaneamente las competencia técnicas y conductuales requeridas para el desempeño del empleo.");
            //evalDes.Add(Chunk.NEWLINE); evalDes.Add(Chunk.NEWLINE);
            docEvaPol.Add(evalDes);
            #endregion

            #region Titulo Competencias Tecnicas
            PdfPTable tableTecnicas = new PdfPTable(1);
            tableTecnicas.TotalWidth = 560f;
            tableTecnicas.LockedWidth = true;

            float[] widthTecnicas = new float[] { 1f };
            tableTecnicas.SetWidths(widthTecnicas);
            tableTecnicas.HorizontalAlignment = 0;
            tableTecnicas.SpacingBefore = 20f;
            //tableTecnicas.SpacingAfter = 30f;

            PdfPCell cellTituloTituloTecnico = new PdfPCell(new Phrase("1. COMPETENCIAS TECNICAS", new Font(Font.FontFamily.HELVETICA, 10f, Font.BOLDITALIC)));
            cellTituloTituloTecnico.HorizontalAlignment = 1;
            cellTituloTituloTecnico.BackgroundColor = new iTextSharp.text.BaseColor(234, 236, 238);
            cellTituloTituloTecnico.BorderColor = new iTextSharp.text.BaseColor(0, 0, 0);
            cellTituloTituloTecnico.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;

            tableTecnicas.AddCell(cellTituloTituloTecnico);

            docEvaPol.Add(tableTecnicas);

            #endregion

            #region Datos Competencias Tecnicas
            PdfPTable tableComTec = new PdfPTable(3);
            tableComTec.TotalWidth = 560;
            tableComTec.LockedWidth = true;

            float[] widths = new float[] { 3f, 1f, 3f };
            tableComTec.SetWidths(widths);
            tableComTec.HorizontalAlignment = 0; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            tableComTec.SpacingAfter = 30f;
            tableComTec.SpacingBefore = 20f;
            //tablaFirmas.DefaultCell.Border = 0; //-------------------- Borde?

            //----------------------------------------------------------------------------------------------------------------------------------------------------------------------
            PdfPCell cel1_1 = new PdfPCell(new Phrase("ETAPA PREVIA A LA APLICACIÓN DE EVALUACION", new Font(Font.FontFamily.HELVETICA, 10f, Font.BOLD)))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0
            };
            PdfPCell cel1_2 = new PdfPCell(new Phrase("CAL.", new Font(Font.FontFamily.HELVETICA, 10f, Font.BOLD)))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                Border = 0
            };
            PdfPCell cel1_3 = new PdfPCell(new Phrase("OBSERVACIONES", new Font(Font.FontFamily.HELVETICA, 10f, Font.BOLD)))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0
            };
            //----------------------------------------------------------------------------------------------------------------------------------------------------------------------

            PdfPCell cel2_1 = new PdfPCell(new Phrase("Corrobora antecedentes de evaluación, en su caso lee reportes de evaluaciones previas.", new Font(Font.FontFamily.HELVETICA, 10f, Font.NORMAL)))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0
            };
            PdfPCell cel2_2 = new PdfPCell(new Phrase(datosEvaluacionPoligrafista.caltec01.ToString(), new Font(Font.FontFamily.HELVETICA, 10f, Font.NORMAL)))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                Border = 0
            };
            PdfPCell cel2_3 = new PdfPCell(new Phrase(datosEvaluacionPoligrafista.observatec01, new Font(Font.FontFamily.HELVETICA, 10f, Font.NORMAL)))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0
            };
            //----------------------------------------------------------------------------------------------------------------------------------------------------------------------

            PdfPCell cel3_1 = new PdfPCell(new Phrase("PRETEST", new Font(Font.FontFamily.HELVETICA, 10f, Font.BOLD)))
            {
                HorizontalAlignment = Element.ALIGN_JUSTIFIED,
                Border = 0
            };
            PdfPCell cel3_2 = new PdfPCell(new Phrase("", new Font(Font.FontFamily.HELVETICA, 10f, Font.NORMAL)))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0
            };
            PdfPCell cel3_3 = new PdfPCell(new Phrase("", new Font(Font.FontFamily.HELVETICA, 10f, Font.NORMAL)))
            {
                HorizontalAlignment = Element.ALIGN_JUSTIFIED,
                Border = 0
            };
            //----------------------------------------------------------------------------------------------------------------------------------------------------------------------

            PdfPCell cel4_1 = new PdfPCell(new Phrase("Establece rapport, revisa datos generales (identifica 4 preguntas comparativas), tiene una sensibilización productiva, plantea una explicación clara del funcionamineto del polígrafo, realiza una adecuada revisión de antecedentes, así como una aplicación de la prueba de ajuste con una posterior retroalimentación productiva para el examen.", new Font(Font.FontFamily.HELVETICA, 10f, Font.NORMAL)))
            {
                HorizontalAlignment = Element.ALIGN_JUSTIFIED,
                Border = 0
            };
            PdfPCell cel4_2 = new PdfPCell(new Phrase(datosEvaluacionPoligrafista.caltec02.ToString(), new Font(Font.FontFamily.HELVETICA, 10f, Font.NORMAL)))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                Border = 0
            };
            PdfPCell cel4_3 = new PdfPCell(new Phrase(datosEvaluacionPoligrafista.observatec02, new Font(Font.FontFamily.HELVETICA, 10f, Font.NORMAL)))
            {
                HorizontalAlignment = Element.ALIGN_JUSTIFIED,
                Border = 0
            };
            //----------------------------------------------------------------------------------------------------------------------------------------------------------------------

            PdfPCell cel5_1 = new PdfPCell(new Phrase("ENTREVISTA", new Font(Font.FontFamily.HELVETICA, 10f, Font.BOLD)))
            {
                HorizontalAlignment = Element.ALIGN_JUSTIFIED,
                Border = 0
            };
            PdfPCell cel5_2 = new PdfPCell(new Phrase("", new Font(Font.FontFamily.HELVETICA, 10f, Font.NORMAL)))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0
            };
            PdfPCell cel5_3 = new PdfPCell(new Phrase("", new Font(Font.FontFamily.HELVETICA, 10f, Font.NORMAL)))
            {
                HorizontalAlignment = Element.ALIGN_JUSTIFIED,
                Border = 0
            };
            //----------------------------------------------------------------------------------------------------------------------------------------------------------------------

            PdfPCell cel6_1 = new PdfPCell(new Phrase("Realiza preguntas acordes abiertas con el objetivo claro de obtener información, los cierres de área los ejemplifica y adapta a la capacidad de entendimiento de cada evaluado, replantea la pregunte cuando así se requiere, realiza una adecuada introducción de preguntas comparativas, las desarrolla de manera convincente y respetuosa.", new Font(Font.FontFamily.HELVETICA, 10f, Font.NORMAL)))
            {
                HorizontalAlignment = Element.ALIGN_JUSTIFIED,
                Border = 0
            };
            PdfPCell cel6_2 = new PdfPCell(new Phrase(datosEvaluacionPoligrafista.caltec03.ToString(), new Font(Font.FontFamily.HELVETICA, 10f, Font.NORMAL)))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                Border = 0
            };
            PdfPCell cel6_3 = new PdfPCell(new Phrase(datosEvaluacionPoligrafista.observatec03, new Font(Font.FontFamily.HELVETICA, 10f, Font.NORMAL)))
            {
                HorizontalAlignment = Element.ALIGN_JUSTIFIED,
                Border = 0
            };
            //----------------------------------------------------------------------------------------------------------------------------------------------------------------------

            PdfPCell cel7_1 = new PdfPCell(new Phrase("FORMULACION DE PREGUNTAS", new Font(Font.FontFamily.HELVETICA, 10f, Font.BOLD)))
            {
                HorizontalAlignment = Element.ALIGN_JUSTIFIED,
                Border = 0
            };
            PdfPCell cel7_2 = new PdfPCell(new Phrase("", new Font(Font.FontFamily.HELVETICA, 10f, Font.NORMAL)))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0
            };
            PdfPCell cel7_3 = new PdfPCell(new Phrase("", new Font(Font.FontFamily.HELVETICA, 10f, Font.NORMAL)))
            {
                HorizontalAlignment = Element.ALIGN_JUSTIFIED,
                Border = 0
            };
            //----------------------------------------------------------------------------------------------------------------------------------------------------------------------

            PdfPCell cel8_1 = new PdfPCell(new Phrase("Pondera las área de riesgo por evaluado, verifica lo que el evaluado entiende en cada pregunta, en semántica respeta la longitud equitativa de ambos tipos de preguntas, respecta el orden correcto de revisión de acuerdo a la técnica empleada.", new Font(Font.FontFamily.HELVETICA, 10f, Font.NORMAL)))
            {
                HorizontalAlignment = Element.ALIGN_JUSTIFIED,
                Border = 0
            };
            PdfPCell cel8_2 = new PdfPCell(new Phrase(datosEvaluacionPoligrafista.caltec04.ToString(), new Font(Font.FontFamily.HELVETICA, 10f, Font.NORMAL)))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                Border = 0
            };
            PdfPCell cel8_3 = new PdfPCell(new Phrase(datosEvaluacionPoligrafista.observatec04, new Font(Font.FontFamily.HELVETICA, 10f, Font.NORMAL)))
            {
                HorizontalAlignment = Element.ALIGN_JUSTIFIED,
                Border = 0
            };
            //----------------------------------------------------------------------------------------------------------------------------------------------------------------------

            PdfPCell cel9_1 = new PdfPCell(new Phrase("IN TEST", new Font(Font.FontFamily.HELVETICA, 10f, Font.BOLD)))
            {
                HorizontalAlignment = Element.ALIGN_JUSTIFIED,
                Border = 0
            };
            PdfPCell cel9_2 = new PdfPCell(new Phrase("", new Font(Font.FontFamily.HELVETICA, 10f, Font.NORMAL)))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0
            };
            PdfPCell cel9_3 = new PdfPCell(new Phrase("", new Font(Font.FontFamily.HELVETICA, 10f, Font.NORMAL)))
            {
                HorizontalAlignment = Element.ALIGN_JUSTIFIED,
                Border = 0
            };
            //----------------------------------------------------------------------------------------------------------------------------------------------------------------------

            PdfPCell cel10_1 = new PdfPCell(new Phrase("Da instrucciones claras y precisas, sus registros están centrados y tienen sensibilidad suficiente para poder observarlos claramente, realiza anotaciones en los gráficos(artefactos, instrucciones) verifica secuencias de preguntas para cada gráfico y obtiene registros estables.", new Font(Font.FontFamily.HELVETICA, 10f, Font.NORMAL)))
            {
                HorizontalAlignment = Element.ALIGN_JUSTIFIED,
                Border = 0
            };
            PdfPCell cel10_2 = new PdfPCell(new Phrase(datosEvaluacionPoligrafista.caltec05.ToString(), new Font(Font.FontFamily.HELVETICA, 10f, Font.NORMAL)))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                Border = 0
            };
            PdfPCell cel10_3 = new PdfPCell(new Phrase(datosEvaluacionPoligrafista.observatec05, new Font(Font.FontFamily.HELVETICA, 10f, Font.NORMAL)))
            {
                HorizontalAlignment = Element.ALIGN_JUSTIFIED,
                Border = 0
            };
            //----------------------------------------------------------------------------------------------------------------------------------------------------------------------

            PdfPCell cel11_1 = new PdfPCell(new Phrase("POSTEST", new Font(Font.FontFamily.HELVETICA, 10f, Font.BOLD)))
            {
                HorizontalAlignment = Element.ALIGN_JUSTIFIED,
                Border = 0
            };
            PdfPCell cel11_2 = new PdfPCell(new Phrase("", new Font(Font.FontFamily.HELVETICA, 10f, Font.NORMAL)))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0
            };
            PdfPCell cel11_3 = new PdfPCell(new Phrase("", new Font(Font.FontFamily.HELVETICA, 10f, Font.NORMAL)))
            {
                HorizontalAlignment = Element.ALIGN_JUSTIFIED,
                Border = 0
            };
            //----------------------------------------------------------------------------------------------------------------------------------------------------------------------

            PdfPCell cel12_1 = new PdfPCell(new Phrase("Ponderar si el evaluador obtuvo información, en segundo término calificar si utilizó una confrontación positiva, desarrolló temas, superó las objeciones, utilizó la justificación, minimización, convenció al evaluado y le dio una razón para otorgar la admisión.", new Font(Font.FontFamily.HELVETICA, 10f, Font.NORMAL)))
            {
                HorizontalAlignment = Element.ALIGN_JUSTIFIED,
                Border = 0
            };
            PdfPCell cel12_2 = new PdfPCell(new Phrase(datosEvaluacionPoligrafista.caltec06.ToString(), new Font(Font.FontFamily.HELVETICA, 10f, Font.NORMAL)))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                Border = 0
            };
            PdfPCell cel12_3 = new PdfPCell(new Phrase(datosEvaluacionPoligrafista.observatec06, new Font(Font.FontFamily.HELVETICA, 10f, Font.NORMAL)))
            {
                HorizontalAlignment = Element.ALIGN_JUSTIFIED,
                Border = 0
            };
            //----------------------------------------------------------------------------------------------------------------------------------------------------------------------

            PdfPCell cel13_1 = new PdfPCell(new Phrase("ELABORACION Y ENTREGA DE REPORTE DE EVALUACION", new Font(Font.FontFamily.HELVETICA, 10f, Font.BOLD)))
            {
                HorizontalAlignment = Element.ALIGN_JUSTIFIED,
                Border = 0
            };
            PdfPCell cel13_2 = new PdfPCell(new Phrase("", new Font(Font.FontFamily.HELVETICA, 10f, Font.NORMAL)))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0
            };
            PdfPCell cel13_3 = new PdfPCell(new Phrase("", new Font(Font.FontFamily.HELVETICA, 10f, Font.NORMAL)))
            {
                HorizontalAlignment = Element.ALIGN_JUSTIFIED,
                Border = 0
            };
            //----------------------------------------------------------------------------------------------------------------------------------------------------------------------

            PdfPCell _blank1_1 = new PdfPCell(new Phrase(".", new Font(Font.FontFamily.HELVETICA, 10f, Font.NORMAL)))
            {
                HorizontalAlignment = Element.ALIGN_JUSTIFIED,
                Border = 0
            };
            PdfPCell _blank1_2 = new PdfPCell(new Phrase("", new Font(Font.FontFamily.HELVETICA, 10f, Font.NORMAL)))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0
            };
            PdfPCell _blank1_3 = new PdfPCell(new Phrase("", new Font(Font.FontFamily.HELVETICA, 10f, Font.NORMAL)))
            {
                HorizontalAlignment = Element.ALIGN_JUSTIFIED,
                Border = 0
            };
            //----------------------------------------------------------------------------------------------------------------------------------------------------------------------

            PdfPCell cel14_1 = new PdfPCell(new Phrase("Verifica que sus reportes tengan una redacción clara, concreta y clasificada, con la propuesta de un resultado apegado a criterios de evaluación. Verifica que al entregar sus expedientes estos tengan un orden.", new Font(Font.FontFamily.HELVETICA, 10f, Font.NORMAL)))
            {
                HorizontalAlignment = Element.ALIGN_JUSTIFIED,
                Border = 0
            };
            PdfPCell cel14_2 = new PdfPCell(new Phrase(datosEvaluacionPoligrafista.caltec06.ToString(), new Font(Font.FontFamily.HELVETICA, 10f, Font.NORMAL)))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                Border = 0
            };
            PdfPCell cel14_3 = new PdfPCell(new Phrase(datosEvaluacionPoligrafista.observatec06, new Font(Font.FontFamily.HELVETICA, 10f, Font.NORMAL)))
            {
                HorizontalAlignment = Element.ALIGN_JUSTIFIED,
                Border = 0
            };
            //----------------------------------------------------------------------------------------------------------------------------------------------------------------------

            tableComTec.AddCell(cel1_1); tableComTec.AddCell(cel1_2); tableComTec.AddCell(cel1_3);
            tableComTec.AddCell(cel2_1); tableComTec.AddCell(cel2_2); tableComTec.AddCell(cel2_3);
            tableComTec.AddCell(cel3_1); tableComTec.AddCell(cel3_2); tableComTec.AddCell(cel3_3);
            tableComTec.AddCell(cel4_1); tableComTec.AddCell(cel4_2); tableComTec.AddCell(cel4_3);
            tableComTec.AddCell(cel5_1); tableComTec.AddCell(cel5_2); tableComTec.AddCell(cel5_3);
            tableComTec.AddCell(cel6_1); tableComTec.AddCell(cel6_2); tableComTec.AddCell(cel6_3);
            tableComTec.AddCell(cel7_1); tableComTec.AddCell(cel7_2); tableComTec.AddCell(cel7_3);
            tableComTec.AddCell(cel8_1); tableComTec.AddCell(cel8_2); tableComTec.AddCell(cel8_3);
            tableComTec.AddCell(cel9_1); tableComTec.AddCell(cel9_2); tableComTec.AddCell(cel9_3);
            tableComTec.AddCell(cel10_1); tableComTec.AddCell(cel10_2); tableComTec.AddCell(cel10_3);
            tableComTec.AddCell(cel11_1); tableComTec.AddCell(cel11_2); tableComTec.AddCell(cel11_3);
            tableComTec.AddCell(cel12_1); tableComTec.AddCell(cel12_2); tableComTec.AddCell(cel12_3);
            tableComTec.AddCell(_blank1_1); tableComTec.AddCell(_blank1_2); tableComTec.AddCell(_blank1_3);
            tableComTec.AddCell(_blank1_1); tableComTec.AddCell(_blank1_2); tableComTec.AddCell(_blank1_3);
            tableComTec.AddCell(_blank1_1); tableComTec.AddCell(_blank1_2); tableComTec.AddCell(_blank1_3);
            tableComTec.AddCell(_blank1_1); tableComTec.AddCell(_blank1_2); tableComTec.AddCell(_blank1_3);
            tableComTec.AddCell(cel13_1); tableComTec.AddCell(cel13_2); tableComTec.AddCell(cel13_3);
            tableComTec.AddCell(cel14_1); tableComTec.AddCell(cel14_2); tableComTec.AddCell(cel14_3);

            docEvaPol.Add(tableComTec);

            #endregion

            #region Titulo Competencias Conductuales
            PdfPTable tableTituloConductal = new PdfPTable(1);
            tableTituloConductal.TotalWidth = 560;
            tableTituloConductal.LockedWidth = true;

            float[] widthTituloConductual = new float[] { 1f };
            tableTituloConductal.SetWidths(widthTituloConductual);
            tableTituloConductal.HorizontalAlignment = 0;
            //tableTituloConductal.SpacingBefore = 20f;
            tableTituloConductal.SpacingAfter = 20f;

            PdfPCell cellTituloConductual = new PdfPCell(new Phrase("2. COMPETENCIAS CONDUCTUALES", new Font(Font.FontFamily.HELVETICA, 10f, Font.BOLDITALIC)));
            cellTituloConductual.HorizontalAlignment = 1;
            cellTituloConductual.BackgroundColor = new iTextSharp.text.BaseColor(234, 236, 238);
            cellTituloConductual.BorderColor = new iTextSharp.text.BaseColor(0, 0, 0);
            cellTituloConductual.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;

            tableTituloConductal.AddCell(cellTituloConductual);

            docEvaPol.Add(tableTituloConductal);
            #endregion

            #region Datos Competencia Conductual
            PdfPTable tbleComCon = new PdfPTable(3);
            tbleComCon.TotalWidth = 560;
            tbleComCon.LockedWidth = true;

            float[] widthConductual = new float[] { 3f, 1f, 3f };
            tbleComCon.SetWidths(widthConductual);
            tbleComCon.HorizontalAlignment = 0;
            //tbleComCon.SpacingBefore = 10f;
            tbleComCon.SpacingAfter = 40f;

            PdfPCell con1a = new PdfPCell(new Phrase("CUALIDADES", new Font(Font.FontFamily.HELVETICA, 10f, Font.BOLD)))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0
            };
            PdfPCell con1b = new PdfPCell(new Phrase("", new Font(Font.FontFamily.HELVETICA, 10f, Font.BOLD)))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0
            };
            PdfPCell con1c = new PdfPCell(new Phrase("", new Font(Font.FontFamily.HELVETICA, 10f, Font.BOLD)))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0
            };

            PdfPCell con2a = new PdfPCell(new Phrase("Capacidad para seguir instrucciones, ejecuta las tareas con rápidez y de manera adecuada, se integra al trabajo en equipo, tiene capaciadad de análisis, es ordenado en su trabajo, aprovecha adecuadamente el equipo y los recursos.", new Font(Font.FontFamily.HELVETICA, 10f, Font.NORMAL)))
            {
                HorizontalAlignment = Element.ALIGN_JUSTIFIED,
                Border = 0
            };
            PdfPCell con2b = new PdfPCell(new Phrase(datosEvaluacionPoligrafista.calcon01.ToString(), new Font(Font.FontFamily.HELVETICA, 10f, Font.NORMAL)))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                Border = 0
            };
            PdfPCell con2c = new PdfPCell(new Phrase(datosEvaluacionPoligrafista.observacon01, new Font(Font.FontFamily.HELVETICA, 10f, Font.NORMAL)))
            {
                HorizontalAlignment = Element.ALIGN_JUSTIFIED,
                Border = 0
            };

            PdfPCell con3a = new PdfPCell(new Phrase("HABILIDADES DE RELACIONES HUMANAS", new Font(Font.FontFamily.HELVETICA, 10f, Font.BOLD)))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0
            };
            PdfPCell con3b = new PdfPCell(new Phrase("", new Font(Font.FontFamily.HELVETICA, 10f, Font.NORMAL)))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0
            };
            PdfPCell con3c = new PdfPCell(new Phrase("", new Font(Font.FontFamily.HELVETICA, 10f, Font.NORMAL)))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0
            };

            PdfPCell con4a = new PdfPCell(new Phrase("Dirige su trabajo al logro de objetivos, detecta las necesidades que se requieren de su puesto y la cubre, escucha de forma adecuada las indicaciones o sugerencias a su trabajo.", new Font(Font.FontFamily.HELVETICA, 10f, Font.NORMAL)))
            {
                HorizontalAlignment = Element.ALIGN_JUSTIFIED,
                Border = 0
            };
            PdfPCell con4b = new PdfPCell(new Phrase(datosEvaluacionPoligrafista.calcon02.ToString(), new Font(Font.FontFamily.HELVETICA, 10f, Font.NORMAL)))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                Border = 0
            };
            PdfPCell con4c = new PdfPCell(new Phrase(datosEvaluacionPoligrafista.observacon02, new Font(Font.FontFamily.HELVETICA, 10f, Font.NORMAL)))
            {
                HorizontalAlignment = Element.ALIGN_JUSTIFIED,
                Border = 0
            };

            PdfPCell con5a = new PdfPCell(new Phrase("HABILIDADES INTERPERSONALES", new Font(Font.FontFamily.HELVETICA, 10f, Font.BOLD)))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0
            };
            PdfPCell con5b = new PdfPCell(new Phrase("", new Font(Font.FontFamily.HELVETICA, 10f, Font.NORMAL)))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0
            };
            PdfPCell con5c = new PdfPCell(new Phrase("", new Font(Font.FontFamily.HELVETICA, 10f, Font.NORMAL)))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0
            };

            PdfPCell con6a = new PdfPCell(new Phrase("Cumple con el horario establecido de llegada a su oficina, así como con el de inicio de las evaluaciones, muestra una actitud reservada y prudente para actuar con juicio en el manejo de datos o situaciones que acontecen en su área de trabajo, respeta los procedimientos, a sus compañeros y sus superiores. Tiene la habilidad para cumplir con el trabajo bajo presión de forma eficiente, sin mostrarse ansioso o agresivo. Muestra iniciativa al proponer soluciones o mejoras.", new Font(Font.FontFamily.HELVETICA, 10f, Font.NORMAL)))
            {
                HorizontalAlignment = Element.ALIGN_JUSTIFIED,
                Border = 0
            };
            PdfPCell con6b = new PdfPCell(new Phrase(datosEvaluacionPoligrafista.calcon03.ToString(), new Font(Font.FontFamily.HELVETICA, 10f, Font.NORMAL)))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                Border = 0
            };
            PdfPCell con6c = new PdfPCell(new Phrase(datosEvaluacionPoligrafista.observacon03, new Font(Font.FontFamily.HELVETICA, 10f, Font.NORMAL)))
            {
                HorizontalAlignment = Element.ALIGN_JUSTIFIED,
                Border = 0
            };

            tbleComCon.AddCell(con1a); tbleComCon.AddCell(con1b); tbleComCon.AddCell(con1c);
            tbleComCon.AddCell(con2a); tbleComCon.AddCell(con2b); tbleComCon.AddCell(con2c);
            tbleComCon.AddCell(con3a); tbleComCon.AddCell(con3b); tbleComCon.AddCell(con3c);
            tbleComCon.AddCell(con4a); tbleComCon.AddCell(con4b); tbleComCon.AddCell(con4c);
            tbleComCon.AddCell(con5a); tbleComCon.AddCell(con5b); tbleComCon.AddCell(con5c);
            tbleComCon.AddCell(con6a); tbleComCon.AddCell(con6b); tbleComCon.AddCell(con6c);

            docEvaPol.Add(tbleComCon);

            #endregion

            #region totales
            Paragraph totales = new Paragraph();
            totales.Alignment = Element.ALIGN_LEFT;

            totales.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            totales.Add("TOTAL COMPETENCIAS TECNICAS");
            totales.Add(Chunk.TABBING); totales.Add(Chunk.TABBING);
            totales.Font = FontFactory.GetFont("Arial", 10, Font.BOLD + Font.UNDERLINE);
            totales.Add((datosEvaluacionPoligrafista.caltec01 + datosEvaluacionPoligrafista.caltec02 + datosEvaluacionPoligrafista.caltec03 + datosEvaluacionPoligrafista.caltec04 + datosEvaluacionPoligrafista.caltec05 + datosEvaluacionPoligrafista.caltec06 + datosEvaluacionPoligrafista.caltec07).ToString());
            totales.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            totales.Add(Chunk.TABBING); totales.Add(Chunk.TABBING);
            totales.Add("TOTAL COMPETENCIAS CONDUCTUALES");
            totales.Add(Chunk.TABBING);
            totales.Font = FontFactory.GetFont("Arial", 10, Font.BOLD + Font.UNDERLINE);
            totales.Add((datosEvaluacionPoligrafista.calcon01 + datosEvaluacionPoligrafista.calcon02 + datosEvaluacionPoligrafista.calcon03).ToString());
            totales.Add(Chunk.NEWLINE);
            totales.Font = FontFactory.GetFont("Arial", 8, Font.NORMAL);
            totales.Add(datosEvaluacionPoligrafista.usuario);
            #endregion

            docEvaPol.Add(totales);

            docEvaPol.Close();

            byte[] bytesStream = ms.ToArray();

            ms = new MemoryStream();
            ms.Write(bytesStream, 0, bytesStream.Length);

            ms.Position = 0;

            return new FileStreamResult(ms, "application/pdf");
        }

        public ActionResult printTestSupervisor(int id, int opcion)
        {
            Test datosEvaluacionSupervisor = Test.getEvaluacionDesempenoImprimir(id, opcion).FirstOrDefault();

            MemoryStream ms = new MemoryStream();

            Document docEvaSup = new Document(PageSize.LETTER, 30f, 20f, 50f, 40f);
            PdfWriter pwEvalSup = PdfWriter.GetInstance(docEvaSup, ms);

            pwEvalSup.PageEvent = new HeaderFooterDesempenoSupervisor();

            docEvaSup.Open();

            Chunk chunk = new Chunk();

            #region Fecha
            Paragraph poligrafista = new Paragraph();
            poligrafista.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            poligrafista.Add("_________________________________________________________________________________________________");
            poligrafista.Add(Chunk.NEWLINE);
            poligrafista.Font = FontFactory.GetFont("Arial", 10, Font.BOLD);
            poligrafista.Add("DEPENDENCIA: CENTRO ESTATAL DE CONTROL DE CONFIANZA CERTIFICADO");
            poligrafista.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            poligrafista.Add(Chunk.TABBING);
            poligrafista.Add(Chunk.TABBING);
            poligrafista.Add(datosEvaluacionSupervisor.fechaTest);
            poligrafista.Add(Chunk.NEWLINE); poligrafista.Add(Chunk.NEWLINE);

            docEvaSup.Add(poligrafista);
            #endregion

            #region Supervisor
            Paragraph evaluado = new Paragraph();
            evaluado.Alignment = Element.ALIGN_CENTER;
            evaluado.Font = FontFactory.GetFont("Arial", 10, Font.BOLD);
            evaluado.Add("Datos del Evaluado");
            evaluado.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            evaluado.Add(Chunk.NEWLINE);

            docEvaSup.Add(evaluado);
            Paragraph evaluado2 = new Paragraph();
            evaluado2.Alignment = Element.ALIGN_LEFT;
            evaluado2.Font = FontFactory.GetFont("Arial", 10, Font.BOLD);
            evaluado2.Add("Nombre completo: ");
            evaluado2.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL+Font.UNDERLINE);
            evaluado2.Add(Chunk.TABBING); evaluado2.Add(Chunk.TABBING);
            evaluado2.Add(datosEvaluacionSupervisor.evaluado);
            evaluado2.Add(Chunk.NEWLINE);
            evaluado2.Font = FontFactory.GetFont("Arial", 10, Font.BOLD);
            evaluado2.Add("Cargo: ");
            evaluado2.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            evaluado2.Add("Supervisor");

            docEvaSup.Add(evaluado2);

            #endregion

            #region datosEvaluacion
            Paragraph evalDes = new Paragraph();
            evalDes.Alignment = Element.ALIGN_JUSTIFIED;
            evalDes.Add(Chunk.NEWLINE);
            evalDes.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            evalDes.Add("Los factores a evaluar, van orientados, no sólo hacia la evaluación del desempeño laboral, sino que pretenden evaluar simúltaneamente las competencia técnicas y conductuales requeridas para el desempeño del empleo");
            //evalDes.Add(Chunk.NEWLINE); evalDes.Add(Chunk.NEWLINE);
            docEvaSup.Add(evalDes);
            #endregion

            #region Titulo Competencias Tecnicas
            PdfPTable tableTecnicas = new PdfPTable(1);
            tableTecnicas.TotalWidth = 560f;
            tableTecnicas.LockedWidth = true;

            float[] widthTecnicas = new float[] { 1f };
            tableTecnicas.SetWidths(widthTecnicas);
            tableTecnicas.HorizontalAlignment = 0;
            tableTecnicas.SpacingBefore = 20f;
            tableTecnicas.SpacingAfter = 10f;

            PdfPCell cellTituloTituloTecnico = new PdfPCell(new Phrase("1.- COMPETENCIAS TECNICAS", new Font(Font.FontFamily.HELVETICA, 10f, Font.BOLDITALIC)));
            cellTituloTituloTecnico.HorizontalAlignment = 1;
            cellTituloTituloTecnico.BackgroundColor = new iTextSharp.text.BaseColor(234, 236, 238);
            cellTituloTituloTecnico.BorderColor = new iTextSharp.text.BaseColor(0, 0, 0);
            cellTituloTituloTecnico.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;

            tableTecnicas.AddCell(cellTituloTituloTecnico);

            docEvaSup.Add(tableTecnicas);

            #endregion

            #region Datos Competencias Tecnicas
            PdfPTable tblConSup = new PdfPTable(3);
            tblConSup.TotalWidth = 560;
            tblConSup.LockedWidth = true;

            float[] widths = new float[] { 3f, 1f, 3f };
            tblConSup.SetWidths(widths);
            tblConSup.HorizontalAlignment = 0; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            tblConSup.SpacingAfter = 20f;
            tblConSup.SpacingBefore = 10f;

            PdfPCell cel1a = new PdfPCell(new Phrase("ETAPA PREVIA AL MONITOREO DE CASOS", new Font(Font.FontFamily.HELVETICA, 10f, Font.BOLD)))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0
            };
            PdfPCell cel1b = new PdfPCell(new Phrase("CAL.", new Font(Font.FontFamily.HELVETICA, 10f, Font.BOLD)))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                Border = 0
            };
            PdfPCell cel1c = new PdfPCell(new Phrase("OBSERVACIONES", new Font(Font.FontFamily.HELVETICA, 10f, Font.BOLD)))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0
            };

            PdfPCell cel2a = new PdfPCell(new Phrase("Se deberá considerar el cumplimiento de registro y asignación de casos, si tiene un plan de monitoreo, si advierte sobre antecedentes y si comparte sobre estrategias generales para abordar la evaluacion.", new Font(Font.FontFamily.HELVETICA, 10f, Font.NORMAL)))
            {
                HorizontalAlignment = Element.ALIGN_JUSTIFIED,
                Border = 0
            };
            PdfPCell cel2b = new PdfPCell(new Phrase(datosEvaluacionSupervisor.caltec01.ToString(), new Font(Font.FontFamily.HELVETICA, 10f, Font.NORMAL)))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                Border = 0
            };
            PdfPCell cel2c = new PdfPCell(new Phrase(datosEvaluacionSupervisor.observatec01, new Font(Font.FontFamily.HELVETICA, 10f, Font.NORMAL)))
            {
                HorizontalAlignment = Element.ALIGN_JUSTIFIED,
                Border = 0
            };

            PdfPCell cel3a = new PdfPCell(new Phrase("DURANTE EL PROCESO DE EVALUACION", new Font(Font.FontFamily.HELVETICA, 10f, Font.BOLD)))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0
            };
            PdfPCell cel3b = new PdfPCell(new Phrase("", new Font(Font.FontFamily.HELVETICA, 10f, Font.BOLD)))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                Border = 0
            };
            PdfPCell cel3c = new PdfPCell(new Phrase("", new Font(Font.FontFamily.HELVETICA, 10f, Font.BOLD)))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0
            };

            PdfPCell cel4a = new PdfPCell(new Phrase("Da seguimiento al evaluador durante entrevista realizando propuestas para mejorar los objetivos de la misma, lleva a cabo una revisión de las preguntas planteadas y en su momento hace sugerencias pertinentes y con un sentido claro. Asimismo efectúa el control de calidad en calificación de gráficos mostrando conocimiento y apego a los criterios de calificación.", new Font(Font.FontFamily.HELVETICA, 10f, Font.NORMAL)))
            {
                HorizontalAlignment = Element.ALIGN_JUSTIFIED,
                Border = 0
            };
            PdfPCell cel4b = new PdfPCell(new Phrase(datosEvaluacionSupervisor.caltec02.ToString(), new Font(Font.FontFamily.HELVETICA, 10f, Font.NORMAL)))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                Border = 0
            };
            PdfPCell cel4c = new PdfPCell(new Phrase(datosEvaluacionSupervisor.observatec02, new Font(Font.FontFamily.HELVETICA, 10f, Font.NORMAL)))
            {
                HorizontalAlignment = Element.ALIGN_JUSTIFIED,
                Border = 0
            };

            PdfPCell cel5a = new PdfPCell(new Phrase("POSTERIOR AL PROCESO DE EVALUACION", new Font(Font.FontFamily.HELVETICA, 10f, Font.BOLD)))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0
            };
            PdfPCell cel5b = new PdfPCell(new Phrase("", new Font(Font.FontFamily.HELVETICA, 10f, Font.BOLD)))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                Border = 0
            };
            PdfPCell cel5c = new PdfPCell(new Phrase("", new Font(Font.FontFamily.HELVETICA, 10f, Font.BOLD)))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0
            };

            PdfPCell cel6a = new PdfPCell(new Phrase("Colabora con el evaluador aportando sus conocimientos en la materia y comparte estrategias para promover la obtención de información.", new Font(Font.FontFamily.HELVETICA, 10f, Font.NORMAL)))
            {
                HorizontalAlignment = Element.ALIGN_JUSTIFIED,
                Border = 0
            };
            PdfPCell cel6b = new PdfPCell(new Phrase(datosEvaluacionSupervisor.caltec03.ToString(), new Font(Font.FontFamily.HELVETICA, 10f, Font.NORMAL)))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                Border = 0
            };
            PdfPCell cel6c = new PdfPCell(new Phrase(datosEvaluacionSupervisor.observatec03, new Font(Font.FontFamily.HELVETICA, 10f, Font.NORMAL)))
            {
                HorizontalAlignment = Element.ALIGN_JUSTIFIED,
                Border = 0
            };

            PdfPCell cel7a = new PdfPCell(new Phrase("ELABORACION Y ENTREGA DE REPORTE DE EVALUACION", new Font(Font.FontFamily.HELVETICA, 10f, Font.BOLD)))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0
            };
            PdfPCell cel7b = new PdfPCell(new Phrase("", new Font(Font.FontFamily.HELVETICA, 10f, Font.BOLD)))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                Border = 0
            };
            PdfPCell cel7c = new PdfPCell(new Phrase("", new Font(Font.FontFamily.HELVETICA, 10f, Font.BOLD)))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0
            };

            PdfPCell cel8a = new PdfPCell(new Phrase("Verifica que los reportes contengan información clara, concreta y clasificada, con la propuesta de un resultado apegado a criterios de evaluación. Libera los resultados de forma oportuna.", new Font(Font.FontFamily.HELVETICA, 10f, Font.NORMAL)))
            {
                HorizontalAlignment = Element.ALIGN_JUSTIFIED,
                Border = 0
            };
            PdfPCell cel8b = new PdfPCell(new Phrase(datosEvaluacionSupervisor.caltec04.ToString(), new Font(Font.FontFamily.HELVETICA, 10f, Font.NORMAL)))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                Border = 0
            };
            PdfPCell cel8c = new PdfPCell(new Phrase(datosEvaluacionSupervisor.observatec04, new Font(Font.FontFamily.HELVETICA, 10f, Font.NORMAL)))
            {
                HorizontalAlignment = Element.ALIGN_JUSTIFIED,
                Border = 0
            };

            tblConSup.AddCell(cel1a); tblConSup.AddCell(cel1b); tblConSup.AddCell(cel1c);
            tblConSup.AddCell(cel2a); tblConSup.AddCell(cel2b); tblConSup.AddCell(cel2c);
            tblConSup.AddCell(cel3a); tblConSup.AddCell(cel3b); tblConSup.AddCell(cel3c);
            tblConSup.AddCell(cel4a); tblConSup.AddCell(cel4b); tblConSup.AddCell(cel4c);
            tblConSup.AddCell(cel5a); tblConSup.AddCell(cel5b); tblConSup.AddCell(cel5c);
            tblConSup.AddCell(cel6a); tblConSup.AddCell(cel6b); tblConSup.AddCell(cel6c);
            tblConSup.AddCell(cel7a); tblConSup.AddCell(cel7b); tblConSup.AddCell(cel7c);
            tblConSup.AddCell(cel8a); tblConSup.AddCell(cel8b); tblConSup.AddCell(cel8c);

            docEvaSup.Add(tblConSup);

            #endregion

            #region Titulo Competencias Conductual
            PdfPTable tableConductual = new PdfPTable(1);
            tableConductual.TotalWidth = 560f;
            tableConductual.LockedWidth = true;

            float[] widthConducta = new float[] { 1f };
            tableConductual.SetWidths(widthConducta);
            tableConductual.HorizontalAlignment = 0;
            tableConductual.SpacingBefore = 10f;
            tableConductual.SpacingAfter = 30f;

            PdfPCell cellTituloTituloconductualsupervisor = new PdfPCell(new Phrase("2.- COMPETENCIAS CONDUCTUALES", new Font(Font.FontFamily.HELVETICA, 10f, Font.BOLDITALIC)));
            cellTituloTituloconductualsupervisor.HorizontalAlignment = 1;
            cellTituloTituloconductualsupervisor.BackgroundColor = new iTextSharp.text.BaseColor(234, 236, 238);
            cellTituloTituloconductualsupervisor.BorderColor = new iTextSharp.text.BaseColor(0, 0, 0);
            cellTituloTituloconductualsupervisor.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;

            tableConductual.AddCell(cellTituloTituloconductualsupervisor);

            docEvaSup.Add(tableConductual);

            #endregion

            #region Datos Competencia Conductual
            PdfPTable tblComCon = new PdfPTable(3);
            tblComCon.TotalWidth = 560;
            tblComCon.LockedWidth = true;

            float[] widthsConductual = new float[] { 3f, 1f, 3f };
            tblComCon.SetWidths(widths);
            tblComCon.HorizontalAlignment = 0; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            tblComCon.SpacingAfter = 10f;
            tblComCon.SpacingBefore = 10f;

            PdfPCell cel9a = new PdfPCell(new Phrase("HABILIDADES ESTRATEGICAS", new Font(Font.FontFamily.HELVETICA, 10f, Font.BOLD)))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0
            };
            PdfPCell cel9b = new PdfPCell(new Phrase("", new Font(Font.FontFamily.HELVETICA, 10f, Font.BOLD)))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0
            };
            PdfPCell cel9c = new PdfPCell(new Phrase("", new Font(Font.FontFamily.HELVETICA, 10f, Font.BOLD)))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0
            };

            PdfPCell cel10a = new PdfPCell(new Phrase("Planea, organiza y establece controles con su equipo de trabajo, propone y aplica planes o estrategias de mejora.", new Font(Font.FontFamily.HELVETICA, 10f, Font.NORMAL)))
            {
                HorizontalAlignment = Element.ALIGN_JUSTIFIED,
                Border = 0
            };
            PdfPCell cel10b = new PdfPCell(new Phrase(datosEvaluacionSupervisor.calcon01.ToString(), new Font(Font.FontFamily.HELVETICA, 10f, Font.NORMAL)))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                Border = 0
            };
            PdfPCell cel10c = new PdfPCell(new Phrase(datosEvaluacionSupervisor.observacon01, new Font(Font.FontFamily.HELVETICA, 10f, Font.NORMAL)))
            {
                HorizontalAlignment = Element.ALIGN_JUSTIFIED,
                Border = 0
            };

            PdfPCell cel11a = new PdfPCell(new Phrase("HABILIDADES DE RELACIONES HUMANAS", new Font(Font.FontFamily.HELVETICA, 10f, Font.BOLD)))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0
            };
            PdfPCell cel11b = new PdfPCell(new Phrase("", new Font(Font.FontFamily.HELVETICA, 10f, Font.BOLD)))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0
            };
            PdfPCell cel11c = new PdfPCell(new Phrase("", new Font(Font.FontFamily.HELVETICA, 10f, Font.BOLD)))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0
            };

            PdfPCell cel12a = new PdfPCell(new Phrase("Dirige a sus subalternos hacia el logro de objetivos, capacidad para integrar su grupo de trabajo, estímula y fomenta la comunicación con su equipo de trabajo, toma decisiones y delega funciones.", new Font(Font.FontFamily.HELVETICA, 10f, Font.NORMAL)))
            {
                HorizontalAlignment = Element.ALIGN_JUSTIFIED,
                Border = 0
            };
            PdfPCell cel12b = new PdfPCell(new Phrase(datosEvaluacionSupervisor.calcon02.ToString(), new Font(Font.FontFamily.HELVETICA, 10f, Font.NORMAL)))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                Border = 0
            };
            PdfPCell cel12c = new PdfPCell(new Phrase(datosEvaluacionSupervisor.observacon02, new Font(Font.FontFamily.HELVETICA, 10f, Font.NORMAL)))
            {
                HorizontalAlignment = Element.ALIGN_JUSTIFIED,
                Border = 0
            };

            PdfPCell cel13a = new PdfPCell(new Phrase("HABILIDADES INTRAPERSONALES", new Font(Font.FontFamily.HELVETICA, 10f, Font.BOLD)))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0
            };
            PdfPCell cel13b = new PdfPCell(new Phrase("", new Font(Font.FontFamily.HELVETICA, 10f, Font.BOLD)))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0
            };
            PdfPCell cel13c = new PdfPCell(new Phrase("", new Font(Font.FontFamily.HELVETICA, 10f, Font.BOLD)))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0
            };

            PdfPCell cel14a = new PdfPCell(new Phrase("Cumple y estimula a su grupo al cumplimiento de horarios, es reservado y prudente en el manejo de información sensible, se vincula e interactúa con respecto, trabaja bajo presión sin tornarse agresivo o voluble.", new Font(Font.FontFamily.HELVETICA, 10f, Font.NORMAL)))
            {
                HorizontalAlignment = Element.ALIGN_JUSTIFIED,
                Border = 0
            };
            PdfPCell cel14b = new PdfPCell(new Phrase(datosEvaluacionSupervisor.calcon03.ToString(), new Font(Font.FontFamily.HELVETICA, 10f, Font.NORMAL)))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                Border = 0
            };
            PdfPCell cel14c = new PdfPCell(new Phrase(datosEvaluacionSupervisor.observacon03, new Font(Font.FontFamily.HELVETICA, 10f, Font.NORMAL)))
            {
                HorizontalAlignment = Element.ALIGN_JUSTIFIED,
                Border = 0
            };

            PdfPCell cel15a = new PdfPCell(new Phrase("_", new Font(Font.FontFamily.HELVETICA, 10f, Font.NORMAL)))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0
            };
            PdfPCell cel15b = new PdfPCell(new Phrase("", new Font(Font.FontFamily.HELVETICA, 10f, Font.NORMAL)))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0
            };
            PdfPCell cel15c = new PdfPCell(new Phrase("", new Font(Font.FontFamily.HELVETICA, 10f, Font.NORMAL)))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0
            };

            tblComCon.AddCell(cel9a); tblComCon.AddCell(cel9b); tblComCon.AddCell(cel9c);
            tblComCon.AddCell(cel10a); tblComCon.AddCell(cel10b); tblComCon.AddCell(cel10c);
            tblComCon.AddCell(cel15a); tblComCon.AddCell(cel15b); tblComCon.AddCell(cel15c);
            tblComCon.AddCell(cel11a); tblComCon.AddCell(cel11b); tblComCon.AddCell(cel11c);
            tblComCon.AddCell(cel12a); tblComCon.AddCell(cel12b); tblComCon.AddCell(cel12c);
            tblComCon.AddCell(cel15a); tblComCon.AddCell(cel15b); tblComCon.AddCell(cel15c);
            tblComCon.AddCell(cel15a); tblComCon.AddCell(cel15b); tblComCon.AddCell(cel15c);
            tblComCon.AddCell(cel13a); tblComCon.AddCell(cel13b); tblComCon.AddCell(cel13c);
            tblComCon.AddCell(cel14a); tblComCon.AddCell(cel14b); tblComCon.AddCell(cel14c);

            docEvaSup.Add(tblComCon);

            #endregion

            #region totales
            Paragraph totales = new Paragraph();
            totales.Alignment = Element.ALIGN_LEFT;

            totales.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            totales.Add("TOTAL COMPETENCIAS TECNICAS");
            totales.Add(Chunk.TABBING); totales.Add(Chunk.TABBING);
            totales.Font = FontFactory.GetFont("Arial", 10, Font.BOLD + Font.UNDERLINE);
            totales.Add((datosEvaluacionSupervisor.caltec01 + datosEvaluacionSupervisor.caltec02 + datosEvaluacionSupervisor.caltec03 + datosEvaluacionSupervisor.caltec04).ToString());
            totales.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            totales.Add(Chunk.TABBING); totales.Add(Chunk.TABBING);
            totales.Add("TOTAL COMPETENCIAS CONDUCTUALES");
            totales.Add(Chunk.TABBING);
            totales.Font = FontFactory.GetFont("Arial", 10, Font.BOLD + Font.UNDERLINE);
            totales.Add((datosEvaluacionSupervisor.calcon01 + datosEvaluacionSupervisor.calcon02 + datosEvaluacionSupervisor.calcon03).ToString());
            totales.Add(Chunk.NEWLINE);
            totales.Font = FontFactory.GetFont("Arial", 8, Font.NORMAL);
            totales.Add(datosEvaluacionSupervisor.usuario);
            #endregion

            docEvaSup.Add(totales);

            docEvaSup.Close();

            byte[] bytesStream = ms.ToArray();

            ms = new MemoryStream();
            ms.Write(bytesStream, 0, bytesStream.Length);

            ms.Position = 0;

            return new FileStreamResult(ms, "application/pdf");
        }

        public ActionResult printSeguimientoId(int id)
        {
            Seguimiento datosSeguimiento = Seguimiento.getSeguimientoId(id).FirstOrDefault();

            MemoryStream ms = new MemoryStream();

            Document docSeguimiento = new Document(PageSize.LETTER, 30f, 20f, 50f, 40f);
            PdfWriter pwSeguimiento = PdfWriter.GetInstance(docSeguimiento, ms);

            pwSeguimiento.PageEvent = new HeaderSeguimiento();

            docSeguimiento.Open();

            Chunk chunk = new Chunk();

            #region Titulos
            Paragraph Titulos = new Paragraph();
            Titulos.Font = FontFactory.GetFont("Arial", 10, Font.BOLD);
            Titulos.Add(Chunk.NEWLINE);
            Titulos.Add("CERTIFICACION CHISCP1BV311018");
            Titulos.Add(Chunk.TABBING); Titulos.Add(Chunk.TABBING); Titulos.Add(Chunk.TABBING); Titulos.Add(Chunk.TABBING);
            Titulos.Add("ACREDITACION No. CHISAC2AV217017");
            Titulos.Add(Chunk.NEWLINE); Titulos.Add(Chunk.NEWLINE);

            docSeguimiento.Add(Titulos);
            #endregion

            #region Fecha
            Paragraph fecha = new Paragraph();
            fecha.Alignment = Element.ALIGN_RIGHT;
            fecha.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            fecha.Add("Fecha: "); fecha.Add(datosSeguimiento.fecha);
            fecha.Add(Chunk.NEWLINE); fecha.Add(Chunk.NEWLINE);
            docSeguimiento.Add(fecha);            
            #endregion

            #region datosEvaluador
            Paragraph eval = new Paragraph();
            eval.Alignment = Element.ALIGN_LEFT;
            eval.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            eval.Add("Supervisión correspondiente al " + datosSeguimiento.periodo + " trimestre del año "+datosSeguimiento.fecha.Substring(6,4));
            eval.Add(Chunk.NEWLINE); eval.Add(Chunk.NEWLINE);
            eval.Add("AP Evaluador: "); eval.Add(Chunk.TABBING);
            eval.Font = FontFactory.GetFont("Arial", 10, Font.BOLD);
            eval.Add(datosSeguimiento.apEvaluador);
            eval.Add(Chunk.NEWLINE);
            eval.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            eval.Add("Nombre: "); eval.Add(Chunk.TABBING);
            eval.Font = FontFactory.GetFont("Arial", 10, Font.BOLD);
            eval.Add(datosSeguimiento.Evaluador);
            eval.Add(Chunk.NEWLINE);
            eval.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            eval.Add("Supervisor: "); eval.Add(Chunk.TABBING);
            eval.Font = FontFactory.GetFont("Arial", 10, Font.BOLD);
            eval.Add(datosSeguimiento.apSupervisor);
            eval.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            eval.Add(Chunk.NEWLINE); eval.Add(Chunk.NEWLINE);
            docSeguimiento.Add(eval);
            #endregion

            #region fortalezas
            Paragraph fortalezas = new Paragraph();
            fortalezas.Font = FontFactory.GetFont("Arial", 10, Font.BOLD);
            fortalezas.Add("Fortalezas");
            fortalezas.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            fortalezas.Add(Chunk.NEWLINE);
            fortalezas.Alignment = Element.ALIGN_JUSTIFIED;
            fortalezas.Add(datosSeguimiento.fortaleza);
            fortalezas.Add(Chunk.NEWLINE); fortalezas.Add(Chunk.NEWLINE);
            docSeguimiento.Add(fortalezas);
            #endregion

            #region areaOportunidad
            Paragraph areaOportunidad = new Paragraph();
            areaOportunidad.Font = FontFactory.GetFont("Arial", 10, Font.BOLD);
            areaOportunidad.Add("Áreas de oportunidad");
            areaOportunidad.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            areaOportunidad.Add(Chunk.NEWLINE);
            areaOportunidad.Alignment = Element.ALIGN_JUSTIFIED;
            areaOportunidad.Add(datosSeguimiento.oportunidad);
            areaOportunidad.Add(Chunk.NEWLINE); areaOportunidad.Add(Chunk.NEWLINE);
            docSeguimiento.Add(areaOportunidad);
            #endregion

            docSeguimiento.Close();

            byte[] bytesStream = ms.ToArray();

            ms = new MemoryStream();
            ms.Write(bytesStream, 0, bytesStream.Length);

            ms.Position = 0;

            return new FileStreamResult(ms, "application/pdf");
        }

        public ActionResult donwloadCarta(int carta)
        {
            //servidor
            string path = @"\10.10.100.25\c$\inetpub\wwwroot\poligramvc\polCartas\CartaCompromiso_"+carta.ToString()+".pdf";
            //proyecto local
            //string path = @"C:\Net 2012\poligramvc\poligramvc\polCartas\CartaCompromiso_" + carta.ToString() + ".pdf";

            return File(path, "application/pdf");
        }

        public ActionResult printVericacionAdmisión(int idVerificacion)
        {
            var datosVerificacion = Admision.getAdmisionImprimir(idVerificacion).FirstOrDefault();

            MemoryStream ms = new MemoryStream();

            //                                                                 izq der top      bot                                    
            Document documentVerificacAdmision = new Document(PageSize.LETTER, 30, 20, 30 + 50, 40);

            PdfWriter pwAdmision = PdfWriter.GetInstance(documentVerificacAdmision, ms);

            pwAdmision.PageEvent = new PDFFooter();

            documentVerificacAdmision.Open();

            #region Formato derecho
            Chunk chunk = new Chunk();
            Paragraph derecha = new Paragraph();
            derecha.Alignment = Element.ALIGN_RIGHT;
            derecha.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            derecha.Add("FORMATO NO. : ");
            derecha.Font = FontFactory.GetFont("Arial", 10, Font.BOLD);
            derecha.Add(datosVerificacion.id.ToString());
            derecha.Add(Chunk.NEWLINE);

            documentVerificacAdmision.Add(derecha);
            #endregion

            #region datos generales
            Paragraph datosGenerales = new Paragraph();
            datosGenerales.Alignment = Element.ALIGN_LEFT;
            datosGenerales.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            datosGenerales.Add("FECHA REVISION :");
            datosGenerales.Add(Chunk.TABBING);
            datosGenerales.Font = FontFactory.GetFont("Arial", 10, Font.BOLD + Font.UNDERLINE);
            datosGenerales.Add(datosVerificacion.fecha);
            datosGenerales.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            datosGenerales.Add(Chunk.NEWLINE);
            datosGenerales.Add("POLIGRAFISTA :");
            datosGenerales.Add(Chunk.TABBING);
            datosGenerales.Font = FontFactory.GetFont("Arial", 10, Font.BOLD + Font.UNDERLINE);
            datosGenerales.Add(datosVerificacion.poligrafista);
            datosGenerales.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            datosGenerales.Add(Chunk.NEWLINE);
            datosGenerales.Add("EVALUADO :");
            datosGenerales.Add(Chunk.TABBING);
            datosGenerales.Font = FontFactory.GetFont("Arial", 10, Font.BOLD + Font.UNDERLINE);
            datosGenerales.Add(datosVerificacion.evaluado);
            datosGenerales.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            datosGenerales.Add(Chunk.NEWLINE);
            datosGenerales.Add("FOLIO :");
            datosGenerales.Add(Chunk.TABBING);
            datosGenerales.Font = FontFactory.GetFont("Arial", 10, Font.BOLD + Font.UNDERLINE);
            datosGenerales.Add(datosVerificacion.serial);
            datosGenerales.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            datosGenerales.Add(Chunk.NEWLINE);
            datosGenerales.Add("HORARIO DE INFORMACIÓN :");
            datosGenerales.Add(Chunk.TABBING);
            datosGenerales.Font = FontFactory.GetFont("Arial", 10, Font.BOLD + Font.UNDERLINE);
            datosGenerales.Add(datosVerificacion.horario);
            datosGenerales.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL);

            documentVerificacAdmision.Add(datosGenerales);
            #endregion

            #region Titulo Informacion
            PdfPTable tableTituloDatoGeneral = new PdfPTable(1);
            tableTituloDatoGeneral.TotalWidth = 560f;
            tableTituloDatoGeneral.LockedWidth = true;

            tableTituloDatoGeneral.SetWidths(widthsTitulosGenerales);
            tableTituloDatoGeneral.HorizontalAlignment = 0;
            tableTituloDatoGeneral.SpacingBefore = 10f;
            tableTituloDatoGeneral.SpacingAfter = 10f;

            PdfPCell cellTituloTituloDato = new PdfPCell(new Phrase("INFORMACIÓN VERIFICADA", new Font(Font.FontFamily.HELVETICA, 10f, Font.BOLDITALIC)));
            cellTituloTituloDato.HorizontalAlignment = 1;
            cellTituloTituloDato.BackgroundColor = new iTextSharp.text.BaseColor(234, 236, 238);
            cellTituloTituloDato.BorderColor = new iTextSharp.text.BaseColor(0, 0, 0);
            cellTituloTituloDato.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
            tableTituloDatoGeneral.AddCell(cellTituloTituloDato);

            documentVerificacAdmision.Add(tableTituloDatoGeneral);
            #endregion

            #region SubTitulo Admision
            Paragraph admision = new Paragraph();
            admision.Add(Chunk.NEWLINE);
            admision.Alignment = Element.ALIGN_LEFT;
            admision.Font = FontFactory.GetFont("Arial", 10, Font.BOLD + Font.UNDERLINE);
            admision.Add("ADMISION");            

            documentVerificacAdmision.Add(admision);
            #endregion

            #region Contenido Admision
            Paragraph contenidoAdmision = new Paragraph();
            contenidoAdmision.Alignment = Element.ALIGN_JUSTIFIED;
            contenidoAdmision.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            contenidoAdmision.Add(Chunk.NEWLINE);
            contenidoAdmision.Add(datosVerificacion.cAdmisiones);
            contenidoAdmision.Add(Chunk.NEWLINE); 

            documentVerificacAdmision.Add(contenidoAdmision);
            #endregion

            #region SubTitulo Observacion General
            Paragraph observacion = new Paragraph();
            observacion.Add(Chunk.NEWLINE);
            observacion.Alignment = Element.ALIGN_LEFT;
            observacion.Font = FontFactory.GetFont("Arial", 10, Font.BOLD + Font.UNDERLINE);
            observacion.Add("OBSERVACIONES GENERALES");

            documentVerificacAdmision.Add(observacion);
            #endregion

            #region Contenido Observacion
            Paragraph contenidoObservacion = new Paragraph();
            contenidoObservacion.Alignment = Element.ALIGN_JUSTIFIED;
            contenidoObservacion.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            contenidoObservacion.Add(Chunk.NEWLINE);
            contenidoObservacion.Add(datosVerificacion.verificacion);
            contenidoObservacion.Add(Chunk.NEWLINE); contenidoObservacion.Add(Chunk.NEWLINE);
            contenidoObservacion.Add(Chunk.NEWLINE); contenidoObservacion.Add(Chunk.NEWLINE);
            contenidoObservacion.Add(Chunk.NEWLINE); contenidoObservacion.Add(Chunk.NEWLINE);
            contenidoObservacion.Add(Chunk.NEWLINE); contenidoObservacion.Add(Chunk.NEWLINE);

            documentVerificacAdmision.Add(contenidoObservacion);
            #endregion

            #region Firmas
            PdfPTable tablaFirmas = new PdfPTable(3);
            tablaFirmas.TotalWidth = 560;
            tablaFirmas.LockedWidth = true;

            float[] widths = new float[] { 1f, 1f, 1f };
            tablaFirmas.SetWidths(widths);
            tablaFirmas.HorizontalAlignment = 0;
            tablaFirmas.SpacingAfter = 20f;
            tablaFirmas.SpacingAfter = 30f;
            tablaFirmas.DefaultCell.Border = 0; //--------------------

            PdfPCell cel1a = new PdfPCell(new Phrase(datosVerificacion.poligrafista, new Font(Font.FontFamily.HELVETICA, 8f, Font.NORMAL)));
            cel1a.HorizontalAlignment = Element.ALIGN_CENTER;
            cel1a.Border = 0;

            PdfPCell cel1b = new PdfPCell(new Phrase(datosVerificacion.supervisor, new Font(Font.FontFamily.HELVETICA, 8f, Font.NORMAL)));
            cel1b.HorizontalAlignment = Element.ALIGN_CENTER;
            cel1b.Border = 0;

            PdfPCell cel1c = new PdfPCell(new Phrase(datosVerificacion.director, new Font(Font.FontFamily.HELVETICA, 8f, Font.NORMAL)));
            cel1c.HorizontalAlignment = Element.ALIGN_CENTER;
            cel1c.Border = 0;

            PdfPCell cel2a = new PdfPCell(new Phrase("Poligrafista", new Font(Font.FontFamily.HELVETICA, 8f, Font.NORMAL)));
            cel2a.HorizontalAlignment = Element.ALIGN_CENTER;
            cel2a.Border = 0;
            //cel2a.Border = 1;

            PdfPCell cel2b = new PdfPCell(new Phrase("Supervisor", new Font(Font.FontFamily.HELVETICA, 8f, Font.NORMAL)));
            cel2b.HorizontalAlignment = Element.ALIGN_CENTER;
            cel2b.Border = 0;
            //cel2a.Border = 1;

            PdfPCell cel2c = new PdfPCell(new Phrase("Director", new Font(Font.FontFamily.HELVETICA, 8f, Font.NORMAL)));
            cel2c.HorizontalAlignment = Element.ALIGN_CENTER;
            cel2c.Border = 0;
            //cel2c.Border = 1;

            tablaFirmas.AddCell(cel1a);
            tablaFirmas.AddCell(cel1b);
            tablaFirmas.AddCell(cel1c);

            tablaFirmas.AddCell(cel2a);
            tablaFirmas.AddCell(cel2b);
            tablaFirmas.AddCell(cel2c);

            documentVerificacAdmision.Add(tablaFirmas);

            #endregion

            documentVerificacAdmision.Close();

            byte[] bytesStream = ms.ToArray();

            ms = new MemoryStream();
            ms.Write(bytesStream, 0, bytesStream.Length);

            ms.Position = 0;

            return new FileStreamResult(ms, "application/pdf");
        }

        class HeaderFooter : PdfPageEventHelper
        {
            public override void OnEndPage(PdfWriter writer, Document document)
            {
                //Header
                PdfPTable tbHeader = new PdfPTable(1); //entre parentesis es el número de columnas
                tbHeader.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
                tbHeader.DefaultCell.Border = 0;

                PdfPCell _cell = new PdfPCell(new Paragraph("Dirección de Poligrafía", FontFactory.GetFont("ARIAL", 12, iTextSharp.text.Font.BOLD)));
                PdfPCell _cell2 = new PdfPCell(new Paragraph("Reporte de Poligrafía", FontFactory.GetFont("ARIAL", 12, iTextSharp.text.Font.BOLD)));
                //PdfPCell _cell3 = new PdfPCell(new Paragraph("CECCC - DPS - F - 09", FontFactory.GetFont("ARIAL", 12, iTextSharp.text.Font.BOLD)));

                _cell.HorizontalAlignment = Element.ALIGN_CENTER;
                _cell2.HorizontalAlignment = Element.ALIGN_CENTER;
                //_cell3.HorizontalAlignment = Element.ALIGN_CENTER;

                _cell.Border = 0;
                _cell2.Border = 0;
                //_cell3.Border = 0;

                tbHeader.AddCell(_cell);
                tbHeader.AddCell(_cell2);
                //tbHeader.AddCell(_cell3);

                tbHeader.AddCell(new Paragraph());

                tbHeader.WriteSelectedRows(0, -1, document.LeftMargin, writer.PageSize.GetTop(document.TopMargin) + 40, writer.DirectContent);

                //Footer
                PdfPTable tbFooter = new PdfPTable(1); //1 columna
                tbFooter.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
                tbFooter.DefaultCell.Border = 0;

                tbFooter.AddCell(new Paragraph());  //celda 1

                _cell = new PdfPCell(new Paragraph("___________________________________________________________________________________", FontFactory.GetFont("ARIAL", 12, iTextSharp.text.Font.BOLD)));
                _cell.HorizontalAlignment = Element.ALIGN_CENTER;
                _cell.Border = 0;
                tbFooter.AddCell(_cell);

                _cell = new PdfPCell(new Paragraph("", FontFactory.GetFont("ARIAL", 8, iTextSharp.text.Font.ITALIC)));
                _cell.HorizontalAlignment = Element.ALIGN_CENTER;
                _cell.Border = 0;

                tbFooter.AddCell(_cell);    //celda 2

                _cell = new PdfPCell(new Paragraph("Página " + writer.PageNumber, FontFactory.GetFont("ARIAL", 8, Font.BOLD)));
                _cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                _cell.Border = 0;

                tbFooter.AddCell(_cell); //celda 3

                tbFooter.WriteSelectedRows(0, -1, document.LeftMargin, writer.PageSize.GetBottom(document.BottomMargin) + 10, writer.DirectContent);
            }
        }

        class HeaderFooterIndice: PdfPageEventHelper
        {
            public override void OnEndPage(PdfWriter writer, Document document)
            {
                //Header
                PdfPTable tbHeaderInd = new PdfPTable(1); //entre parentesis es el número de columnas
                tbHeaderInd.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
                tbHeaderInd.DefaultCell.Border = 0;

                PdfPCell _cell = new PdfPCell(new Paragraph("Dirección de Poligrafía", FontFactory.GetFont("ARIAL", 12, iTextSharp.text.Font.BOLD)));
                PdfPCell _cell2 = new PdfPCell(new Paragraph("Formato Contenido del Expediente", FontFactory.GetFont("ARIAL", 12, iTextSharp.text.Font.BOLD)));
                //PdfPCell _cell3 = new PdfPCell(new Paragraph("CECCC - DPS - F - 09", FontFactory.GetFont("ARIAL", 12, iTextSharp.text.Font.BOLD)));

                _cell.HorizontalAlignment = Element.ALIGN_CENTER;
                _cell2.HorizontalAlignment = Element.ALIGN_CENTER;
                //_cell3.HorizontalAlignment = Element.ALIGN_CENTER;

                _cell.Border = 0;
                _cell2.Border = 0;
                //_cell3.Border = 0;

                tbHeaderInd.AddCell(_cell);
                tbHeaderInd.AddCell(_cell2);
                //tbHeader.AddCell(_cell3);

                tbHeaderInd.AddCell(new Paragraph());

                tbHeaderInd.WriteSelectedRows(0, -1, document.LeftMargin, writer.PageSize.GetTop(document.TopMargin) + 40, writer.DirectContent);
            }
        }

        class HeaderFooterDesempenoPoligrafista : PdfPageEventHelper
        {
            public override void OnEndPage(PdfWriter writer, Document document)
            {
                //Header
                PdfPTable tbHeader = new PdfPTable(1); //entre parentesis es el número de columnas
                tbHeader.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
                tbHeader.DefaultCell.Border = 0;

                PdfPCell _cell = new PdfPCell(new Paragraph("FORMATO DE EVALUACION DEL DESEMPEÑO LABORAL", FontFactory.GetFont("ARIAL", 11, iTextSharp.text.Font.BOLD)));
                PdfPCell _cell2 = new PdfPCell(new Paragraph("PARA EVALUADORES DEL AREA DE POLIGRAFIA", FontFactory.GetFont("ARIAL", 11, iTextSharp.text.Font.BOLD)));
                //PdfPCell _cell3 = new PdfPCell(new Paragraph("CECCC - DPS - F - 09", FontFactory.GetFont("ARIAL", 12, iTextSharp.text.Font.BOLD)));

                _cell.HorizontalAlignment = Element.ALIGN_CENTER;
                _cell2.HorizontalAlignment = Element.ALIGN_CENTER;
                //_cell3.HorizontalAlignment = Element.ALIGN_CENTER;

                _cell.Border = 0;
                _cell2.Border = 0;
                //_cell3.Border = 0;

                tbHeader.AddCell(_cell);
                tbHeader.AddCell(_cell2);
                //tbHeader.AddCell(_cell3);

                tbHeader.AddCell(new Paragraph());

                tbHeader.WriteSelectedRows(0, -1, document.LeftMargin, writer.PageSize.GetTop(document.TopMargin) + 40, writer.DirectContent);

                //Footer
                PdfPTable tbFooter = new PdfPTable(1); //1 columna
                tbFooter.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
                tbFooter.DefaultCell.Border = 0;

                tbFooter.AddCell(new Paragraph());  //celda 1

                _cell = new PdfPCell(new Paragraph("___________________________________________________________________________________", FontFactory.GetFont("ARIAL", 12, iTextSharp.text.Font.BOLD)));
                _cell.HorizontalAlignment = Element.ALIGN_CENTER;
                _cell.Border = 0;
                tbFooter.AddCell(_cell);

                _cell = new PdfPCell(new Paragraph("", FontFactory.GetFont("ARIAL", 8, iTextSharp.text.Font.ITALIC)));
                _cell.HorizontalAlignment = Element.ALIGN_CENTER;
                _cell.Border = 0;

                tbFooter.AddCell(_cell);    //celda 2

                //_cell = new PdfPCell(new Paragraph("Página " + writer.PageNumber, FontFactory.GetFont("ARIAL", 8, Font.BOLD)));
                _cell = new PdfPCell(new Paragraph("Código: CNCA 8037, Versión: 01", FontFactory.GetFont("ARIAL", 8, iTextSharp.text.Font.ITALIC)));
                _cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                _cell.Border = 0;

                tbFooter.AddCell(_cell); //celda 3

                tbFooter.WriteSelectedRows(0, -1, document.LeftMargin, writer.PageSize.GetBottom(document.BottomMargin) + 10, writer.DirectContent);
            }
        }

        class HeaderFooterDesempenoSupervisor : PdfPageEventHelper
        {
            public override void OnEndPage(PdfWriter writer, Document document)
            {
                //Header
                PdfPTable tbHeader = new PdfPTable(1); //entre parentesis es el número de columnas
                tbHeader.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
                tbHeader.DefaultCell.Border = 0;

                PdfPCell _cell = new PdfPCell(new Paragraph("FORMATO DE EVALUACION DEL DESEMPEÑO LABORAL", FontFactory.GetFont("ARIAL", 11, iTextSharp.text.Font.BOLD)));
                PdfPCell _cell2 = new PdfPCell(new Paragraph("PARA SUPERVISORES DEL AREA DE POLIGRAFIA", FontFactory.GetFont("ARIAL", 11, iTextSharp.text.Font.BOLD)));
                //PdfPCell _cell3 = new PdfPCell(new Paragraph("CECCC - DPS - F - 09", FontFactory.GetFont("ARIAL", 12, iTextSharp.text.Font.BOLD)));

                _cell.HorizontalAlignment = Element.ALIGN_CENTER;
                _cell2.HorizontalAlignment = Element.ALIGN_CENTER;
                //_cell3.HorizontalAlignment = Element.ALIGN_CENTER;

                _cell.Border = 0;
                _cell2.Border = 0;
                //_cell3.Border = 0;

                tbHeader.AddCell(_cell);
                tbHeader.AddCell(_cell2);
                //tbHeader.AddCell(_cell3);

                tbHeader.AddCell(new Paragraph());

                tbHeader.WriteSelectedRows(0, -1, document.LeftMargin, writer.PageSize.GetTop(document.TopMargin) + 40, writer.DirectContent);

                //Footer
                PdfPTable tbFooter = new PdfPTable(1); //1 columna
                tbFooter.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
                tbFooter.DefaultCell.Border = 0;

                tbFooter.AddCell(new Paragraph());  //celda 1

                _cell = new PdfPCell(new Paragraph("___________________________________________________________________________________", FontFactory.GetFont("ARIAL", 12, iTextSharp.text.Font.BOLD)));
                _cell.HorizontalAlignment = Element.ALIGN_CENTER;
                _cell.Border = 0;
                tbFooter.AddCell(_cell);

                _cell = new PdfPCell(new Paragraph("", FontFactory.GetFont("ARIAL", 8, iTextSharp.text.Font.ITALIC)));
                _cell.HorizontalAlignment = Element.ALIGN_CENTER;
                _cell.Border = 0;

                tbFooter.AddCell(_cell);    //celda 2

                //_cell = new PdfPCell(new Paragraph("Página " + writer.PageNumber, FontFactory.GetFont("ARIAL", 8, Font.BOLD)));
                _cell = new PdfPCell(new Paragraph("Código: CNCA 8037, Versión: 01", FontFactory.GetFont("ARIAL", 8, iTextSharp.text.Font.ITALIC)));
                _cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                _cell.Border = 0;

                tbFooter.AddCell(_cell); //celda 3

                tbFooter.WriteSelectedRows(0, -1, document.LeftMargin, writer.PageSize.GetBottom(document.BottomMargin) + 10, writer.DirectContent);
            }
        }

        class HeaderSeguimiento : PdfPageEventHelper
        {
            public override void OnEndPage(PdfWriter writer, Document document)
            {
                //Header
                PdfPTable tbHeader = new PdfPTable(1); //entre parentesis es el número de columnas
                tbHeader.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
                tbHeader.DefaultCell.Border = 0;

                PdfPCell _cell = new PdfPCell(new Paragraph("DIRECCION DE POLIGRAFIA", FontFactory.GetFont("ARIAL", 11, iTextSharp.text.Font.BOLD)));

                _cell.HorizontalAlignment = Element.ALIGN_CENTER;

                _cell.Border = 0;

                tbHeader.AddCell(_cell);

                tbHeader.AddCell(new Paragraph());

                tbHeader.WriteSelectedRows(0, -1, document.LeftMargin, writer.PageSize.GetTop(document.TopMargin) + 40, writer.DirectContent);
            }
        }

        class PDFFooter: PdfPageEventHelper
        {
            public override void OnOpenDocument(PdfWriter writer, Document document)
            {
                base.OnOpenDocument(writer, document);
            }

            public override void OnStartPage(PdfWriter writer, Document document)
            {
                base.OnStartPage(writer, document);
            }

            public override void OnEndPage(PdfWriter writer, Document document)
            {
                Rectangle page = document.PageSize;

                var mappedPath = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data");

                string imageizq = mappedPath + "/images/CECCC_red.png";
                iTextSharp.text.Image jpgSupIzq = iTextSharp.text.Image.GetInstance(imageizq);
                jpgSupIzq.ScaleToFit(90f, 90f);

                PdfPCell clLogosupIzq = new PdfPCell();
                clLogosupIzq.BorderWidth = 0;
                clLogosupIzq.AddElement(jpgSupIzq);

                string imageder = mappedPath + "/images/gob_red.png";
                iTextSharp.text.Image jpgSupDer = iTextSharp.text.Image.GetInstance(imageder);
                jpgSupDer.Alignment = iTextSharp.text.Image.ALIGN_RIGHT;
                jpgSupDer.ScaleToFit(100f, 100f);

                PdfPCell clLogoSupDer = new PdfPCell();
                clLogoSupDer.BorderWidth = 0;
                clLogoSupDer.AddElement(jpgSupDer);

                Chunk chkTit = new Chunk("CENTRO ESTATAL DE CONTROL DE CONFIANZA CERTIFICADO", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10f, iTextSharp.text.Font.BOLD, BaseColor.BLACK));
                Paragraph paragraph = new Paragraph();
                paragraph.Alignment = Element.ALIGN_CENTER;
                paragraph.Add(chkTit);

                Chunk chkSub = new Chunk("DIRECCION DE POLIGRAFIA", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10f, iTextSharp.text.Font.BOLD, BaseColor.BLACK));
                Paragraph paragraph1 = new Paragraph();
                paragraph1.Alignment = Element.ALIGN_CENTER;
                paragraph1.Add(chkSub);

                PdfPCell clTitulo = new PdfPCell();
                clTitulo.BorderWidth = 0;
                clTitulo.AddElement(paragraph);

                PdfPCell clSubTit = new PdfPCell();
                clSubTit.BorderWidth = 0;
                clSubTit.AddElement(paragraph1);

                PdfPTable tblTitulo = new PdfPTable(1);
                tblTitulo.WidthPercentage = 100;
                tblTitulo.AddCell(clTitulo);
                tblTitulo.AddCell(clSubTit);

                PdfPCell clTablaTitulo = new PdfPCell();
                clTablaTitulo.BorderWidth = 0;
                clTablaTitulo.VerticalAlignment = Element.ALIGN_MIDDLE;
                clTablaTitulo.AddElement(tblTitulo);

                PdfPTable tblEncabezado = new PdfPTable(3);
                tblEncabezado.WidthPercentage = 100;
                float[] widths = new float[] { 20f, 60f, 20f };
                tblEncabezado.SetWidths(widths);

                tblEncabezado.AddCell(clLogosupIzq);
                tblEncabezado.AddCell(clTablaTitulo);
                tblEncabezado.AddCell(clLogoSupDer);

                base.OnOpenDocument(writer, document);
                PdfPTable tabFot = new PdfPTable(new float[] { 1F });
                tabFot.SpacingAfter = 5F;
                PdfPCell cell;
                tabFot.TotalWidth = 560f;
                cell = new PdfPCell(tblEncabezado);
                cell.Border = Rectangle.NO_BORDER;
                tabFot.AddCell(cell);
                tabFot.WriteSelectedRows(0, -1, 20, document.Top + tabFot.TotalHeight + 10, writer.DirectContent);
                tabFot.SpacingAfter = 30f;

                var fontFooter = FontFactory.GetFont("Verdana", 8, Font.NORMAL, BaseColor.LIGHT_GRAY);
                PdfPTable footer = new PdfPTable(1);
                footer.TotalWidth = page.Width - 70;

                PdfPCell cf1 = new PdfPCell(new Phrase("1a Avenida Sur Oriente No. 290, Col.Centro C.P. 29000; Tuxtla Gutiérrez, Chiapas.", fontFooter));
                cf1.HorizontalAlignment = Element.ALIGN_LEFT;
                //cf1.FixedHeight = cellHeight;
                cf1.Border = PdfPCell.NO_BORDER;
                cf1.BorderWidthTop = 0.75f;
                cf1.BorderColor = BaseColor.LIGHT_GRAY;
                footer.AddCell(cf1);
                //PdfPCell cf2 = new PdfPCell(new Phrase("Conmutador: 961 618 93 00 ext. red. 64009, 64115, 64138", fontFooter));
                //cf2.HorizontalAlignment = Element.ALIGN_LEFT;
                //cf2.Border = PdfPCell.NO_BORDER;
                //footer.AddCell(cf2);
                //PdfPCell cf3 = new PdfPCell(new Phrase("https://www.controldeconfianza.chiapas.gob.mx/", fontFooter));
                //cf3.HorizontalAlignment = Element.ALIGN_LEFT;
                //cf3.Border = PdfPCell.NO_BORDER;
                //footer.AddCell(cf3);
                footer.WriteSelectedRows(0, -1, 20, 50, writer.DirectContent);
            }
        }
    }
}