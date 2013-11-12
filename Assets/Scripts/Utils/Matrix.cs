using UnityEngine;
using System.Collections;

public class Matrix
{
	protected float[,] data;
	protected int w;
	protected int h;
	
	// Constructor
	public Matrix(int w, int h)
	{
		this.w = w;
		this.h = h;
		
		data = new float[w, h];
	}
	
	// Copy constructor
	public Matrix(Matrix m)
	{
		this.w = m.w;
		this.h = m.h;
		
		data = new float[w, h];
		for (int i=0; i<w; i++)
			for (int j=0; j<h; j++)
				data[i, j] = m.data[i, j];
	}
	
	// Width accessor
	public int getW()
	{
		return w;
	}
	
	// Height accessor
	public int getH()
	{
		return h;
	}
	
	// Check ranges
	protected bool isValidIndex(int i, int j)
	{
		return (i >= 0 && i < w && j >= 0 && j < h);
	}
	
	// Get/Set a value
	public float this[int i, int j]
	{
		get
	    {
			if (isValidIndex(i, j))
				return data[i, j];
			return float.NaN;
	    }
	    set
	    {
			if (isValidIndex(i, j))
	        	data[i, j] = value;
	    }
	}
	
	// Swap colomns col1 and col2 of the Matrix
	public virtual void SwapColumns(int col1, int col2)
    {
		if (col1 == col2)
			return;
		
		float tmp;
		for (int i=0; i<getH(); i++)
		{
			tmp = data[col1, i];
			data[col1, i] = data[col2, i];
			data[col2, i] = tmp;
		}
    }

	// Spwap rows row1 and row2 of the Matrix
    public virtual void SwapRows(int row1, int row2)
    {
		if (row1 == row2)
			return;
		
        float tmp;
		for (int i=0; i<getW(); i++)
		{
			tmp = data[i, row1];
			data[i, row1] = data[i, row2];
			data[i, row2] = tmp;
		}
    }

	// Determine the reduce of the Matrix
	public Matrix reduce()
	{
		Matrix reduced = new Matrix(this);
		
        int lead = 0;
        for (int row = 0; row < reduced.getH(); row++)
        {
            if (reduced.getW() <= lead)
                break;
			
            int i = row;
            while (reduced[lead, i] == 0)
            {
                i++;
                if (i == reduced.getH())
                {
                    i = row;
                    lead++;
                    if (lead == reduced.getW())
						return reduced;
                }
            }

            reduced.SwapRows(i, row);
			
			float divFactor = reduced[lead, row];
			for (int k = 0; k < reduced.getW(); k++)
			{
				reduced[k, row] = reduced[k, row] / divFactor;
			}
			
            for (int j = 0; j < reduced.getH(); j++)
            {
                if (j == row) continue;
				
				float factor = reduced[lead, j];
				for (int k = 0; k < reduced.getW(); k++)
				{
					reduced[k, j] = reduced[k, j] - reduced[k, row] * factor;
				}
            }
			
        }
        return reduced;
	}
}
