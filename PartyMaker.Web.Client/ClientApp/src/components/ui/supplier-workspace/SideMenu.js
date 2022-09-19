﻿import { Paper, MenuList, MenuItem, Stack } from '@mui/material';
import React, { useState, useEffect } from 'react'

import Page from "../../pages/supplier-workspace/supplier-workspace";
import '../../pages/supplier-workspace/supplier-workspace.css'

const SupplierWorkspace = () => {

    let tags = ['Active', 'Requests', 'Finished'];

    const [state, setState] = useState(0);
    const handleSelectPage = (selectedPageId) => {
        setState(selectedPageId);
    };

    const getActivePage = () => {
        return <Page id={state} />;
    };

    return (
        <div>
            <h1 >Workspace</h1>
            <Stack direction="row" spacing={2}>
                <Paper className="menu">
                    <MenuList>
                        {
                            tags?.map((tag) => (
                                <MenuItem onClick={() => handleSelectPage(tags.indexOf(tag))}>{tag}</MenuItem>
                            ))
                        }
                    </MenuList>
                </Paper>

                <Paper className="paper-info">
                    {getActivePage()}
                </Paper>

            </Stack>
        </div>
    );
}
export default SupplierWorkspace
