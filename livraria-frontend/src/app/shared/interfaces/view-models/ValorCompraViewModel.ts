import { TipoDeCompra } from "../../enums/tipo-de-compra.enum";

export interface ValorCompraViewModel {
  tipo: TipoDeCompra;
  valor: number;
}
