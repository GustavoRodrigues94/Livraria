import { AssuntoViewModel } from "./AssuntoViewModel";
import { AutorViewModel } from "./AutorViewModel";
import { ValorCompraViewModel } from "./ValorCompraViewModel";

export interface LivroViewModel {
  livroId: number;
  titulo: string;
  editora: string;
  edicao: number;
  anoPublicacao: string;
  autores: AutorViewModel[];
  valoresCompra: ValorCompraViewModel[];
  assuntos: AssuntoViewModel[];
}
