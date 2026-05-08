import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { ProductService } from '../services/product.service';
import { ApiResponse } from '../models/api-response.model';
import { Product } from '../models/product.model';
import { environment } from '../../environments/environments';

describe('ProductService', () => {
  let service: ProductService;
  let httpMock: HttpTestingController;

  // Mock products
  const mockProducts: Product[] = [
    { productId: 1, name: 'Laptop', description: 'Gaming laptop', price: 75000, stock: 10, imageUrl: null, category: 'Electronics' },
    { productId: 2, name: 'T-Shirt', description: 'Cotton t-shirt', price: 500, stock: 50, imageUrl: null, category: 'Fashion' },
    { productId: 3, name: 'iPhone', description: 'Apple phone', price: 90000, stock: 5, imageUrl: null, category: 'Electronics' }
  ];

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [ProductService]
    });

    service = TestBed.inject(ProductService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  //  Creation 
  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  //  getAll 
  it('should fetch all products', () => {
    service.getAll().subscribe(products => {
      expect(products.length).toBe(3);
    });

    const req = httpMock.expectOne(`${environment.gatewayUrl}/products`);
    expect(req.request.method).toBe('GET');

    req.flush({ success: true, data: mockProducts });
  });

  //  getById 
  it('should fetch product by id', () => {
    service.getById(1).subscribe(product => {
      expect(product.productId).toBe(1);
    });

    const req = httpMock.expectOne(`${environment.gatewayUrl}/products/1`);
    expect(req.request.method).toBe('GET');

    req.flush({ success: true, data: mockProducts[0] });
  });

  //  create 
  it('should create product', () => {
    const dto = {
      name: 'Headphones',
      description: 'Wireless',
      price: 5000,
      stock: 20,
      imageUrl: null,
      category: 'Electronics'
    };

    service.create(dto).subscribe(product => {
      expect(product.name).toBe('Headphones');
    });

    const req = httpMock.expectOne(`${environment.gatewayUrl}/products`);
    expect(req.request.method).toBe('POST');

    req.flush({ success: true, data: { productId: 4, ...dto } });
  });

  //  update 
  it('should update product', () => {
    service.update(1, {
      name: 'Updated',
      description: 'Updated',
      price: 1000,
      stock: 5,
      imageUrl: null
    }).subscribe(res => {
      expect(res.success).toBeTrue();
    });

    const req = httpMock.expectOne(`${environment.gatewayUrl}/products/1`);
    expect(req.request.method).toBe('PUT');

    req.flush({ success: true });
  });

  //  delete 
  it('should delete product', () => {
    service.delete(1).subscribe(res => {
      expect(res.success).toBeTrue();
    });

    const req = httpMock.expectOne(`${environment.gatewayUrl}/products/1`);
    expect(req.request.method).toBe('DELETE');

    req.flush({ success: true });
  });

  //  search 
  it('should filter products by search', (done) => {
    service.getAll().subscribe(() => {
      service.search('laptop');

      setTimeout(() => {
        service.filteredProducts$.subscribe((filtered: Product[]) => {
          expect(filtered.length).toBe(1);
          done();
        });
      }, 300);
    });

    httpMock.expectOne(`${environment.gatewayUrl}/products`)
      .flush({ success: true, data: mockProducts });
  });

  //  category filter 
  it('should filter by category', (done) => {
    service.getAll().subscribe(() => {
      service.setCategory('Electronics');

      setTimeout(() => {
        service.filteredProducts$.subscribe((filtered: Product[]) => {
          expect(filtered.every(p => p.category === 'Electronics')).toBeTrue();
          done();
        });
      }, 300);
    });

    httpMock.expectOne(`${environment.gatewayUrl}/products`)
      .flush({ success: true, data: mockProducts });
  });

  //  max price filter 
 it('should filter by max price', (done) => {

  service.getAll().subscribe(() => {

    service.setMaxPrice(1000); // 

    setTimeout(() => {
      service.filteredProducts$.subscribe((filtered: Product[]) => {
        expect(filtered.length).toBe(1);
        expect(filtered[0].name).toBe('T-Shirt');
        done();
      });
    }, 300);

  });

  httpMock.expectOne(`${environment.gatewayUrl}/products`)
    .flush({ success: true, data: mockProducts });

});

});