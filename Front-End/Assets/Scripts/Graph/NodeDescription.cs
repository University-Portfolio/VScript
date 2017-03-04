﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeDescription : MonoBehaviour {

	[System.Serializable]
	public struct NodeIO
	{
		public string Name;
		public Color NameColour;
		public Color SocketColour;
		public SocketDescription.SocketType ExectutionType;
	}

	[SerializeField]
	private TextMesh HeaderText;
	[SerializeField]
	private SpriteRenderer HeaderBar;

	[SerializeField]
	private SocketDescription DefaultSocket;
	private List<SocketDescription> Inputs;
	private List<SocketDescription> Outputs;
	private const float ConnectionLocation = 1.564f;
	private const float ConnectionSpacing = 0.296f;


	public void SetHeaderText(string header)
	{
		HeaderText.text = header;
	}

	public void SetHeaderColour(Color colour)
	{
		HeaderBar.color = colour;
	}

	public void SetHeaderFontSize(float size)
	{
		HeaderText.characterSize = size * 0.03f;
	}

	public void SetHeaderFontColour(Color colour)
	{
		HeaderText.color = colour;
	}

	public void SetInputs(NodeIO[] inputs)
	{
		if (Inputs != null)
		{
			//Remove existing sockets
			foreach (SocketDescription socket in Inputs)
				Destroy(socket.gameObject);
			Inputs.Clear();
		}
		else
			Inputs = new List<SocketDescription>();

		int i = 0;

		//Spawn new sockets
		foreach (NodeIO input in inputs)
		{
			GameObject socket = Instantiate(DefaultSocket.gameObject, transform);

			//Setup socket
			SocketDescription socket_desc = socket.GetComponent<SocketDescription>();
			socket_desc.SetName(input.Name);
			socket_desc.SetNameColour(input.NameColour);
			socket_desc.SetSocketColour(input.SocketColour);
			socket_desc.SetSocketType(input.ExectutionType);
			socket_desc.SetIOType(SocketDescription.IOType.Input);

			socket.transform.position = new Vector2(
				-ConnectionLocation,
				- 0.414f - ConnectionSpacing * i
				);
			socket.transform.position += transform.position;

			Inputs.Add(socket_desc);
			i++;
        }
	}

	public void SetOutputs(NodeIO[] outputs)
	{
		if (Outputs != null)
		{
			//Remove existing sockets
			foreach (SocketDescription socket in Outputs)
				Destroy(socket.gameObject);
			Outputs.Clear();
		}
		else
			Outputs = new List<SocketDescription>();

		int i = 0;

		//Spawn new sockets
		foreach (NodeIO output in outputs)
		{
			GameObject socket = Instantiate(DefaultSocket.gameObject, transform);

			//Setup socket
			SocketDescription socket_desc = socket.GetComponent<SocketDescription>();
			socket_desc.SetName(output.Name);
			socket_desc.SetNameColour(output.NameColour);
			socket_desc.SetSocketColour(output.SocketColour);
			socket_desc.SetSocketType(output.ExectutionType);
			socket_desc.SetIOType(SocketDescription.IOType.Output);

			socket.transform.position = new Vector2(
				ConnectionLocation,
				-0.414f - ConnectionSpacing * i
				);
			socket.transform.position += transform.position;

			Outputs.Add(socket_desc);
			i++;
		}
	}
}
