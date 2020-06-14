import React, { useState, useEffect } from "react";
import { PseudoBox, Box,  Select, Input, FormLabel, FormControl, SimpleGrid, IconButton, Button, useDisclosure, useToast } from "@chakra-ui/core";
import SelectionTable from "./selectionTable";
import styled from '@emotion/styled'
import { useFormik, Field, Form, Formik } from 'formik';
import { Modal,
  ModalOverlay,
  ModalContent,
  ModalHeader,
  ModalFooter,
  ModalBody,
  ModalCloseButton } from "@chakra-ui/core";
import { getApi } from "../utils/getApi"
import { FaEdit as EditIcon } from 'react-icons/fa';



const ErrorLabel = styled.label`
color: #FF0000
`

const CarsTable = (props) => {
  const {isOpen, onOpen, onClose} = useDisclosure();
  const [saveError, setSaveError] = useState(null);
  const [defaultState, setDefaultState] = useState(
    {
        id: null,
        vehicleType: 'Car',
        vehicleCarDetails: {
          detailId: null,
          make: '',
          model: '',
          engine: '',
          doors: 4,
          wheels: 4,
          bodyType: ''
        }
    });
  const toast = useToast();
  


  const saveCar = async(values) => {
    try {
      var url = getApi() + `vehicles`;
      var verb = 'POST';
      values.vehicleCarDetails.wheels = parseInt(values.vehicleCarDetails.wheels);
      values.vehicleCarDetails.doors = parseInt(values.vehicleCarDetails.doors);
      if (values.id != null) {
        url += `/${values.id}`
        verb = 'PUT';
      }
      const response = await fetch(url, {
          method: verb,
          headers: {
              'Content-Type': 'application/json',
            },
          body: JSON.stringify(values),
      });
      if (response.ok) {
        onClose();
      }
      else {
        if (response.status === 401 && props.doLogout) {
          // Redirect to home page
        } 
          var result = await response.text();
        if (result != null) {
          throw new Error(result);
        }
        else {
          throw new Error('Something went wrong');
        }
      }
    } catch (ex) {
      toast({
        title: 'Unable to save',
        description: ex.message,
        status: 'error',
        duration: 5000,
        isClosable: true,
        position: 'top-right',
      });
    }
  }

    const addCarHandler = () => { 
    openModal(null);
  };


  const validateCar = values => {
    const errors = {};
  
    // TODO: Add any cross-field validation here
  
    return errors;
  };

  function validateMake(value) {
    let error;
    if (!value) {
      error = "Make is required";
    }
    return error;
  }

  function validateModel(value) {
    let error;
    if (!value) {
      error = "Model is required";
    }
    return error;
  }

  function validateEngine(value) {
    let error;
    if (!value) {
      error = "Engine specs are required";
    }
    return error;
  }

  function validateWheels(value) {
    let error;
    if (!value) {
      error = "Value required";
    }
    else if (isNaN(value)) {
      error = "Not numeric";
    }
    else if (value < 4) {
      error = "Need 4 or more wheels";
    }
    return error;
  }

  function validateDoors(value) {
    let error;
    if (!value) {
      error = "Value required";
    }
    else if (isNaN(value)) {
      error = "Not numeric";
    }
    else if (value < 1) {
      error = "Need 1 or more wheels";
    }
    return error;
  }

  const openModal = async(id) => {
    if (id == null) {
      setDefaultState({
        id: null,
        vehicleType: "Car",
        vehicleCarDetails: {
          make: "",
          model: "",
          engine: "",
          doors: 4,
          wheels: 4,
          bodyType: "Sedan"
        }
      });

      onOpen();
    }
    else {
      const response = await fetch(getApi() +`vehicles/${id}`, {
        method: 'GET',
        headers: {
        },
      });
      if (response.ok) {
        const json = await response.json();
        setDefaultState(json);

        onOpen();
      }
      else {
        if (response.status === 401 && props.doLogout) {
          // Redirect to login page
        } 
      }
    }
  };  

  const headerFormat = (item) => { 
    return <PseudoBox as="tr">
            {/* <PseudoBox as="th" textAlign="left">FeedbackID</PseudoBox>  */}
            <PseudoBox as="th" textAlign="left"></PseudoBox> 
            <PseudoBox as="th" textAlign="left">ID</PseudoBox> 
            <PseudoBox as="th" textAlign="left">VehicleType</PseudoBox> 
            <PseudoBox as="th" textAlign="left">Make</PseudoBox> 
            <PseudoBox as="th" textAlign="left">Model</PseudoBox> 
            <PseudoBox as="th" textAlign="left">Engine</PseudoBox> 
            <PseudoBox as="th" textAlign="left">Doors</PseudoBox>
            <PseudoBox as="th" textAlign="left">Wheels</PseudoBox>
            <PseudoBox as="th" textAlign="left">BodyType</PseudoBox>
          </PseudoBox>
  };

  const rowFormat = (item) => { 
    return <PseudoBox as="tr" _hover={{ bg: 'red.50' }}  key={item.id}>
            <PseudoBox as="td"  ><IconButton size="xs" variantColor="blue" onClick={() => openModal(item.id)} icon={EditIcon}></IconButton></PseudoBox>
            <PseudoBox as="td">{item.id}</PseudoBox>
            <PseudoBox as="td">{item.vehicleType}</PseudoBox>
            <PseudoBox as="td">{item.vehicleCarDetails?.make}</PseudoBox>
            <PseudoBox as="td">{item.vehicleCarDetails?.model}</PseudoBox>
            <PseudoBox as="td">{item.vehicleCarDetails?.engine}</PseudoBox>
            <PseudoBox as="td">{item.vehicleCarDetails?.doors}</PseudoBox>
            <PseudoBox as="td">{item.vehicleCarDetails?.wheels}</PseudoBox>
            <PseudoBox as="td">{item.vehicleCarDetails?.bodyType}</PseudoBox>
          </PseudoBox>
  };

  return  <Box backgroundColor="#e0e0e0" padding="0px" width="100%" padding="10" >
            <SelectionTable title="Cars" 
                            url="vehicles" 
                            rowFormatter={rowFormat}
                            headerFormat={headerFormat}
                            addHandler={addCarHandler}
                            {...props}
            />
                      
            <Modal isOpen={isOpen} onClose={onClose} size="xl">
              <ModalOverlay />

              <ModalContent>
                  <ModalHeader>Car</ModalHeader>
                  <ModalCloseButton />
                  <ModalBody>
                    <Formik initialValues={defaultState} enableReinitialize={true} validate={validateCar} onSubmit={saveCar} >
                    {props => (
                      <form onSubmit={props.handleSubmit}>
                        <Field name="id">
                          {({ field, form }) => (
                            <FormControl isInvalid={form.errors.id && form.touched.id}>
                              <FormLabel htmlFor="id">ID</FormLabel>
                              <Input {...field} id="id" placeholder="id" isReadOnly={true} />
                            </FormControl>
                          )}
                        </Field>
                        <Field name="vehicleCarDetails.detailId">
                          {({ field, form }) => (
                            <FormControl isInvalid={form.errors.detailId && form.touched.detailId}>
                              <FormLabel htmlFor="detailId">ID</FormLabel>
                              <Input {...field} id="detailId" placeholder="detailId" isReadOnly={true} />
                            </FormControl>
                          )}
                        </Field>
                        <Field name="vehicleCarDetails.make" validate={validateMake}>
                          {({ field, form }) => (
                            <FormControl isInvalid={form.errors.vehicleCarDetails?.make && form.touched.vehicleCarDetails?.make}>
                              <FormLabel htmlFor="make">Make</FormLabel>
                              <Input {...field} id="make" placeholder="make" />
                              {form.errors.vehicleCarDetails?.make ? <ErrorLabel>{form.errors.vehicleCarDetails?.make}</ErrorLabel> : null}
                            </FormControl>
                          )}
                        </Field>

                        <Field name="vehicleCarDetails.model" validate={validateModel}>
                          {({ field, form }) => (
                            <FormControl isInvalid={form.errors.vehicleCarDetails?.model && form.touched.vehicleCarDetails?.model}>
                              <FormLabel htmlFor="model">Model</FormLabel>
                              <Input {...field} id="model" placeholder="model" />
                              {form.errors.vehicleCarDetails?.model ? <ErrorLabel>{form.errors.vehicleCarDetails?.model}</ErrorLabel> : null}
                            </FormControl>
                          )}
                        </Field>


                        <SimpleGrid columns={2} rows={2} spacing={5}>
                          <Field name="vehicleCarDetails.engine" validate={validateEngine}>
                            {({ field, form }) => (
                              <FormControl isInvalid={form.errors.vehicleCarDetails?.engine && form.touched.vehicleCarDetails?.engine}>
                                <FormLabel htmlFor="engine">Engine</FormLabel>
                                <Input {...field} id="engine" placeholder="engine" />
                                {form.errors.vehicleCarDetails?.engine ? <ErrorLabel>{form.errors.vehicleCarDetails?.engine}</ErrorLabel> : null}
                              </FormControl>
                            )}
                          </Field>                          
                          <Field name="vehicleCarDetails.bodyType">
                            {({ field, form }) => (
                              <FormControl isInvalid={form.errors.bodyType && form.touched.bodyType}>
                                <FormLabel htmlFor="bodyType">Body Type</FormLabel>
                                <Select {...field} id="bodyType" >
                                  <option>Sedan</option>
                                  <option>Hatch back</option>
                                  <option>Ute</option>
                                  <option>SUV</option>
                                  <option>Convertable</option>
                                </Select>
                              </FormControl>
                            )}
                          </Field>
                          <Field name="vehicleCarDetails.wheels" validate={validateWheels}>
                            {({ field, form }) => (
                              <FormControl isInvalid={form.errors.vehicleCarDetails?.wheels && form.touched.vehicleCarDetails?.wheels}>
                                <FormLabel htmlFor="wheels">Wheels</FormLabel>
                                <Input {...field} id="wheels" placeholder="engine" />
                                {form.errors.vehicleCarDetails?.wheels ? <ErrorLabel>{form.errors.vehicleCarDetails?.wheels}</ErrorLabel> : null}
                              </FormControl>
                            )}
                          </Field>
                          <Field name="vehicleCarDetails.doors" validate={validateDoors}>
                            {({ field, form }) => (
                              <FormControl isInvalid={form.errors.vehicleCarDetails?.doors && form.touched.vehicleCarDetails?.doors}>
                                <FormLabel htmlFor="doors">Doors</FormLabel>
                                <Input {...field} id="doors" placeholder="doors" />
                                {form.errors.vehicleCarDetails?.doors ? <ErrorLabel>{form.errors.vehicleCarDetails?.doors}</ErrorLabel> : null}
                              </FormControl>
                            )}
                          </Field>
                        </SimpleGrid>


                        <Box textAlign="right" mt={5}>
                          <Button variantColor="teal" mr={3} isLoading={props.isSubmitting} type="submit" >Save</Button>
                          <Button variantColor="blue" onClick={onClose}>Cancel</Button>
                        </Box>

                      </form>
                        )}
                      </Formik>
                  </ModalBody>
              </ModalContent>
            </Modal>            
          </Box>            
}

export default CarsTable
