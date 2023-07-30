import {Spinner} from "reactstrap";

export function Loading() {
  return (
    <div className="d-table fill">
      <div className="d-table-cell center">
        <Spinner />
      </div>
    </div>
  )
}