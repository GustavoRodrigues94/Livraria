import { TipoDeCompra } from "../../../enums/tipo-de-compra.enum";

export interface AlterarLivroCommand {
  livroId: number;
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
