import React, { useState, useEffect } from "react";
import { Heading, Button, Text, ButtonGroup, Box, Flex, Stack, IconButton, FormLabel, Switch, Input } from "@chakra-ui/core";
import { IoIosRefresh as RefreshIcon } from 'react-icons/io';
import { IoIosAddCircleOutline as AddIcon } from 'react-icons/io';
import { getApi } from "../utils/getApi"
import { useTheme } from "@chakra-ui/core";


const SelectionTable = (props) => {
  const [error, setError] = useState(null);
  const [tableData, setTableData] = useState(null);
  const [isLoading, setIsLoading] = useState(false);
  const theme = useTheme();

  const fetchData = async () => {
    try {
      const response = await fetch(getApi() + props.url, {
        method: 'GET',
        headers: {
          // TODO: Add token if authenticated
        }
      });

      if (response.ok) {
          const json = await response.json();
          setTableData(json);
      } else {
          // If 401 returned, redirect to login screen
        const body = await response.json();
        console.log(`not ok later ${response.statusCode}`);
        throw new Error(response.status);
      }
    } catch (error) {
      console.log("ex caught");
      setError("Caught:" + error);
    }
  }  

  useEffect(() => {
    fetchData();
  }, [props.url]);

  const handleRefresh = () => fetchData();

  return  (
      <Box backgroundColor="#e0e0e0" padding="0px" width="100%" hidden={props.hidden}>
        <Stack>
          <Flex width="100%" bg="gray.500" justify="space-between" hidden={props.noHeader != null && props.noHeader}>
            <Heading  color="gray.100" size="lg" >{[props.title]}</Heading>
            <Box align="right">
              <Stack spacing={4} bg="gray.600" padding="2px" direction="row">
                {props.addHandler ? <IconButton size="xs" variantColor="blue" onClick={props.addHandler} icon={AddIcon}></IconButton> : null}
                <IconButton size="xs" variantColor="blue" onClick={handleRefresh} icon={RefreshIcon}></IconButton>
              </Stack> 
            </Box>
          </Flex>

          <table>
            <thead>
              {props.headerFormat()}
            </thead>
            <tbody>
              {tableData != null && tableData.map((item, index) => (
                props.rowFormatter(item)
              ))}
              </tbody>
            </table>
        </Stack> 
      </Box>)
}

export default SelectionTable
