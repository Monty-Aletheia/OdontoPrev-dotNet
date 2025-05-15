#!/bin/bash

kubectl delete all --all --all-namespaces
kubectl delete pvc --all --all-namespaces
kubectl delete configmap --all --all-namespaces
kubectl delete secret --all --all-namespaces
