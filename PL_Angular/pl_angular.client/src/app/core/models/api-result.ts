export interface ApiResult<T> {
  correct: boolean;
  objects?: T[];      // para GetAll
  object?: T;         // para GetById
  errorMessage?: string;
}
