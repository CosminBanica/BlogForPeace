import React, { useEffect } from "react";
import { BrowserRouter, Route, Routes } from "react-router-dom";
import Blogpost from "../pages/Blogpost";
import Blogposts from "../pages/Blogposts";
import Account from "../pages/Account";
import AddBlogpost from "../pages/AddBlogpost";
import { useAuth0 } from "@auth0/auth0-react";

const Router = () => {
  const { isAuthenticated, loginWithRedirect } = useAuth0();

  useEffect(() => {
    if (!isAuthenticated) {
      loginWithRedirect();
    }
  }, [isAuthenticated, loginWithRedirect]);

  return (
    isAuthenticated && (
      <BrowserRouter>
        <Routes>
            <Route exact path="/Account" element={<Account />} />
            <Route exact path='/' element={<Blogposts />} />
            <Route exact path='/Blogpost/:id' element={<Blogpost />} />
            <Route exact path='/AddBlogpost' element={<AddBlogpost />} />
        </Routes>
      </BrowserRouter>
    )
  );
};

export default Router;
