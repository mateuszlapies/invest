import {
  createBrowserRouter,
  RouterProvider,
} from "react-router-dom";

import Home from "./Home";
import ErrorPage from "./ErrorPage";

export default function AppRouter() {
  const router = createBrowserRouter([
    {
      path: "/",
      element: <Home />,
      errorElement: <ErrorPage />
    },
  ]);

  return (
    <RouterProvider router={router} />
  )
}