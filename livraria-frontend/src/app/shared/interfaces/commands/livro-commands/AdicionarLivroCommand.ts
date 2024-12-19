import { TipoDeCompra } from "../../../enums/tipo-de-compra.enum";

export interface AdicionarLivroCommand {
  titulo: string;
  autores: number[];
  editora: string;
  edicao: number;
  anoPublicacao: string;
  valoresCompra: {
    tipo: TipoDeCompra;
    valor: number;
  }[];
  assuntos: string[];
}
