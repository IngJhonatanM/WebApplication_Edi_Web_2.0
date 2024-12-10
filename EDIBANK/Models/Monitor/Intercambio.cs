﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EDIBANK.Models.Monitor;

[Table("intercambios"), Index(nameof(Cargado))]
public class Intercambio
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.None), Column("id_tracking", TypeName = "char(17)", Order = 0), Unicode(false), Comment("YYYYMMDDhhmmsssss")]
    [Display(Name = "#Tracking")]
    public required string Id { get; init; }

    [Column("fec_carga", TypeName = "datetime2", Order = 1)]
    [Display(Name = "Cargado")]
    public required DateTime Cargado { get; init; }

    [Column("fec_descarga", TypeName = "datetime2", Order = 2)]
    [Display(Name = "Descargado")]
    public DateTime? Descargado { get; set; }

    [Column("nro_intercambio", TypeName = "varchar(50)", Order = 3)]
    [Display(Name = "#Documento")]
    public required string Numero { get; init; }

    [Column("tip_intercambio", TypeName = "tinyint", Order = 4)]
    [Display(Name = "Tipo")]
    public required TipoIntercambio TipoIntercambio { get; init; }

    [ForeignKey(nameof(Emisor)), Column("edi_envia", TypeName = "varchar(15)", Order = 5), Unicode(false)]
    [Display(Name = "Emisor")]
    public required string EmisorId { get; init; }

    [ForeignKey(nameof(Receptor)), Column("edi_recibe", TypeName = "varchar(15)", Order = 6), Unicode(false)]
    [Display(Name = "Receptor")]
    public string? ReceptorId { get; init; }

    [Column("tip_documento", TypeName = "varchar(50)", Order = 7)]
    public string? TipoDocumento { get; init; }

    [Column("ori_archivo", TypeName = "varchar(50)", Order = 8)]
    public required string ArchivoOriginal { get; init; }

    [Column("int_archivo", TypeName = "varchar(59)", Order = 9)]
    [Display(Name = "Archivo")]
    public required string ArchivoIntercambio { get; init; }

    [Column("ruta_archivo", TypeName = "varchar(200)", Order = 10)]
    public required string RutaArchivo { get; init; }

    [Column("tamano", TypeName = "bigint", Order = 11)]
    public required long Tamano { get; init; }

    [Column("status", TypeName = "tinyint", Order = 12)]
    [Display(Name = "Estatus")]
    public required Status Status { get; set; }

    public required EDI Emisor { get; init; }

    public EDI? Receptor { get; init; }
}
