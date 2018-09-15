using System.Collections.Generic;

namespace HelpTeacher.Domain.Entities
{
	/// <summary>Define a entidade questão.</summary>
	public class Question : IEntityBase
	{
		#region Properties
		/// <summary><see cref="Entities.Subject"/>'s onde a questão pode ser usada.</summary>
		public virtual ICollection<Subject> Subject { get; set; }

		/// <summary>Caminho completo para o primeiro anexo.</summary>
		public string FirstAttachment { get; set; }

		/// <summary>
		/// Define se a questão é padrão.
		/// <para>
		/// Uma questão definida como padrão sempre será usada ao gerar uma avaliação
		/// para a <see cref="Entities.Subject"/> a qual ela está relacionada.
		/// </para>
		/// </summary>
		public bool IsDefault { get; set; }

		/// <summary>Define se a questão é do fipo objetiva.</summary>
		public bool IsObjective { get; set; }

		/// <summary>Implementa <see cref="IEntityBase.IsRecordActive"/>.</summary>
		public bool IsRecordActive { get; set; }

		/// <summary>Implementa <see cref="IEntityBase.RecordID"/>.</summary>
		public int RecordID { get; set; }

		/// <summary>Caminho completo para o segundo anexo.</summary>
		public string SecondAttachment { get; set; }

		/// <summary>Enunciado da questão.</summary>
		public string Statement { get; set; }

		/// <summary>Define se a questão já foi usada alguma vez.</summary>
		public bool WasUsed { get; set; }
		#endregion


		#region Constructors
		/// <summary>
		/// Inicializa uma nova instância da classe <see cref="Question"/> com os 
		/// <see cref = "Subject" /> e enunciado específicados.
		/// </summary>
		/// <param name="value">Assuntos onde a nova questão pode ser usada.</param>
		/// <param name="statement">Enunciado</param>
		public Question(ICollection<Subject> value, string statement)
		{
			Subject = value;
			Statement = statement;
		}
		#endregion
	}
}
